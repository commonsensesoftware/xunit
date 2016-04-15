namespace Xunit.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a formatter that formats the display name of a class or method into a more human readable form.
    /// </summary>
    public class DisplayNameFormatter
    {
        private readonly CharacterRule rule;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameFormatter"/> class.
        /// </summary>
        /// <param name="display">The <see cref="TestMethodDisplay"/> options used by the formatter.</param>
        public DisplayNameFormatter(TestMethodDisplay display)
        {
            rule = new CharacterRule();

            if ((display & TestMethodDisplay.AllowEscapeSequences) == TestMethodDisplay.AllowEscapeSequences)
            {
                rule = new ReplaceEscapeSequenceRule(rule);
            }

            if ((display & TestMethodDisplay.ReplaceUnderscoreWithSpace) == TestMethodDisplay.ReplaceUnderscoreWithSpace)
            {
                rule = new ReplaceUnderscoreRule(rule);
            }

            if ((display & TestMethodDisplay.AllowOperatorMonikers) == TestMethodDisplay.AllowOperatorMonikers)
            {
                rule = new ReplaceOperatorMonikerRule(rule);
            }
        }

        /// <summary>
        /// Formats the specified display name.
        /// </summary>
        /// <param name="displayName">The display name to format.</param>
        /// <returns>The formatted display name.</returns>
        public string Format(string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                return displayName;
            }

            var context = new FormatContext(displayName);

            while (context.HasMoreText)
            {
                rule.Evaluate(context, context.ReadNext());
            }

            context.Flush();

            return context.FormattedDisplayName.ToString();
        }

        private sealed class FormatContext
        {
            private readonly string text;

            public FormatContext(string text)
            {
                this.text = text;
                Length = text.Length;
            }

            public StringBuilder FormattedDisplayName { get; } = new StringBuilder();

            public StringBuilder Buffer { get; } = new StringBuilder();

            public int Position { get; private set; }

            public int Length { get; }

            public bool HasMoreText => Position < Length;

            public char ReadNext() => text[Position++];

            public void Flush()
            {
                FormattedDisplayName.Append(Buffer);
                Buffer.Clear();
            }
        }

        private class CharacterRule
        {
            public CharacterRule()
            {
            }

            public CharacterRule(CharacterRule next)
            {
                Next = next;
            }

            public virtual void Evaluate(FormatContext context, char character) => context.Buffer.Append(character);

            public CharacterRule Next { get; }
        }

        private sealed class ReplaceOperatorMonikerRule : CharacterRule
        {
            private static readonly Dictionary<string, string> tokenMonikers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "eq", "=" },
                { "ne", "!=" },
                { "lt", "<" },
                { "le", "<=" },
                { "gt", ">" },
                { "ge", ">=" },
            };

            public ReplaceOperatorMonikerRule(CharacterRule next)
                : base(next)
            {
            }

            public override void Evaluate(FormatContext context, char character)
            {
                string token = null;
                string @operator = null;

                if (character != '_')
                {
                    if (context.HasMoreText)
                    {
                        Next?.Evaluate(context, character);
                        return;
                    }
                    else
                    {
                        // note: branching case if we're at the end of the display name
                        token = context.Buffer.ToString() + character;

                        if (!tokenMonikers.TryGetValue(token, out @operator))
                        {
                            Next?.Evaluate(context, character);
                            return;
                        }

                        context.Buffer.Clear();
                        context.Buffer.Append(@operator);
                        context.Flush();
                        return;
                    }
                }

                token = context.Buffer.ToString();

                if (!tokenMonikers.TryGetValue(token, out @operator))
                {
                    Next?.Evaluate(context, character);
                    return;
                }

                context.Buffer.Clear();
                context.Buffer.Append(@operator);
                context.Buffer.Append(' ');
                context.Flush();
            }
        }

        private sealed class ReplaceUnderscoreRule : CharacterRule
        {
            public ReplaceUnderscoreRule(CharacterRule next)
                : base(next)
            {
            }

            public override void Evaluate(FormatContext context, char character)
            {
                if (character != '_')
                {
                    Next?.Evaluate(context, character);
                    return;
                }

                context.Buffer.Append(' ');
                context.Flush();
            }
        }

        private sealed class ReplaceEscapeSequenceRule : CharacterRule
        {
            public ReplaceEscapeSequenceRule(CharacterRule next)
                : base(next)
            {
            }

            public override void Evaluate(FormatContext context, char character)
            {
                switch (character)
                {
                    case 'U':
                        // same as \uHHHH without the leading '\'
                        TryConsumeEscapeSequence(context, character, 4);
                        break;
                    case 'X':
                        // same as \xHH without the leading '\'
                        TryConsumeEscapeSequence(context, character, 2);
                        break;
                    default:
                        Next?.Evaluate(context, character);
                        break;
                }
            }

            private static void TryConsumeEscapeSequence(FormatContext context, char @char, int allowedLength)
            {
                var escapeSequence = new char[allowedLength];
                var consumed = 0;

                while (consumed < allowedLength && context.HasMoreText)
                {
                    var nextChar = context.ReadNext();

                    escapeSequence[consumed++] = nextChar;

                    if (IsHex(nextChar))
                    {
                        continue;
                    }

                    context.Buffer.Append(@char);
                    context.Buffer.Append(escapeSequence, 0, consumed);
                    return;
                }

                context.Buffer.Append(char.ConvertFromUtf32(HexToInt32(escapeSequence)));
            }

            private static bool IsHex(char c) => (c > 64 && c < 71) || (c > 47 && c < 58);

            private static int HexToInt32(char[] hex)
            {
                var @int = 0;
                var length = hex.Length - 1;

                for (var i = 0; i <= length; i++)
                {
                    var c = hex[i];
                    var v = c < 58 ? c - 48 : c - 55;
                    @int += v << ((length - i) << 2);
                }

                return @int;
            }
        }
    }
}