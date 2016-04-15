using System;
using System.Collections.Generic;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.Serialization;
using TestMethodDisplay = Xunit.Sdk.TestMethodDisplay;

public class DisplayNameFormatterTests
{
    private abstract class Example
    {
        public abstract void unit_tests_are_awesomeX21_U263A();
        public abstract void api_version_1_eq_1X2E0();
        public abstract void api_version_should_be_greater_than_1();
        public abstract void api_version_2_should_be_gt_than_1();
        public abstract void api_version_2_should_be_ge_than_1();
        public abstract void api_version_1_should_be_lt_than_2();
        public abstract void api_version_1_should_be_le_than_2();
        public abstract void api_version_1X2E0_should_ne_2U002E0();
        public abstract void lt_0_should_be_an_error();
        public abstract void equals_operator_overload_should_be_same_as_eq_eq();
        public abstract void X3DX3D_operator_overload_should_be_same_as_equals_method();
        public abstract void masculine_super_heroes_should_be_buffed();
        public abstract void termination_date_should_be_updated_when_employee_is_axed();
        public abstract void total_amount_should_be_updated_when_order_is_taxed();
        public abstract void X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27();
        public abstract void X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27();
        public abstract void TestNameShouldRemainUnchanged();
        public abstract void TestX20NameX20WithU0020Spaces();
    }

    private abstract class Given_a_version_number
    {
        public abstract void when_it_equals_1X2C_then_it_should_be_less_than_2();
    }

    public static IEnumerable<object[]> UnformattedDisplayNameData
    {
        get
        {
            yield return new object[] { nameof(Example.unit_tests_are_awesomeX21_U263A), nameof(Example.unit_tests_are_awesomeX21_U263A) };
            yield return new object[] { nameof(Example.api_version_1_eq_1X2E0), nameof(Example.api_version_1_eq_1X2E0) };
            yield return new object[] { nameof(Example.api_version_should_be_greater_than_1), nameof(Example.api_version_should_be_greater_than_1) };
            yield return new object[] { nameof(Example.api_version_2_should_be_gt_than_1), nameof(Example.api_version_2_should_be_gt_than_1) };
            yield return new object[] { nameof(Example.api_version_2_should_be_ge_than_1), nameof(Example.api_version_2_should_be_ge_than_1) };
            yield return new object[] { nameof(Example.api_version_1_should_be_lt_than_2), nameof(Example.api_version_1_should_be_lt_than_2) };
            yield return new object[] { nameof(Example.api_version_1_should_be_le_than_2), nameof(Example.api_version_1_should_be_le_than_2) };
            yield return new object[] { nameof(Example.api_version_1X2E0_should_ne_2U002E0), nameof(Example.api_version_1X2E0_should_ne_2U002E0) };
            yield return new object[] { nameof(Given_a_version_number), nameof(Given_a_version_number) };
            yield return new object[] { nameof(Given_a_version_number.when_it_equals_1X2C_then_it_should_be_less_than_2), nameof(Given_a_version_number.when_it_equals_1X2C_then_it_should_be_less_than_2) };
            yield return new object[] { nameof(Example.lt_0_should_be_an_error), nameof(Example.lt_0_should_be_an_error) };
            yield return new object[] { nameof(Example.equals_operator_overload_should_be_same_as_eq_eq), nameof(Example.equals_operator_overload_should_be_same_as_eq_eq) };
            yield return new object[] { nameof(Example.X3DX3D_operator_overload_should_be_same_as_equals_method), nameof(Example.X3DX3D_operator_overload_should_be_same_as_equals_method) };
            yield return new object[] { nameof(Example.masculine_super_heroes_should_be_buffed), nameof(Example.masculine_super_heroes_should_be_buffed) };
            yield return new object[] { nameof(Example.termination_date_should_be_updated_when_employee_is_axed), nameof(Example.termination_date_should_be_updated_when_employee_is_axed) };
            yield return new object[] { nameof(Example.total_amount_should_be_updated_when_order_is_taxed), nameof(Example.total_amount_should_be_updated_when_order_is_taxed) };
            yield return new object[] { nameof(Example.X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27), nameof(Example.X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27) };
            yield return new object[] { nameof(Example.X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27), nameof(Example.X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27) };
            yield return new object[] { nameof(Example.TestNameShouldRemainUnchanged), nameof(Example.TestNameShouldRemainUnchanged) };
            yield return new object[] { nameof(Example.TestX20NameX20WithU0020Spaces), nameof(Example.TestX20NameX20WithU0020Spaces) };
        }
    }

    public static IEnumerable<object[]> AllFormatExtensionsData
    {
        get
        {
            yield return new object[] { nameof(Example.unit_tests_are_awesomeX21_U263A), "unit tests are awesome! ☺" };
            yield return new object[] { nameof(Example.api_version_1_eq_1X2E0), "api version 1 = 1.0" };
            yield return new object[] { nameof(Example.api_version_should_be_greater_than_1), "api version should be greater than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_gt_than_1), "api version 2 should be > than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_ge_than_1), "api version 2 should be >= than 1" };
            yield return new object[] { nameof(Example.api_version_1_should_be_lt_than_2), "api version 1 should be < than 2" };
            yield return new object[] { nameof(Example.api_version_1_should_be_le_than_2), "api version 1 should be <= than 2" };
            yield return new object[] { nameof(Example.api_version_1X2E0_should_ne_2U002E0), "api version 1.0 should != 2.0" };
            yield return new object[] { nameof(Given_a_version_number), "Given a version number" };
            yield return new object[] { nameof(Given_a_version_number.when_it_equals_1X2C_then_it_should_be_less_than_2), "when it equals 1, then it should be less than 2" };
            yield return new object[] { nameof(Example.lt_0_should_be_an_error), "< 0 should be an error" };
            yield return new object[] { nameof(Example.equals_operator_overload_should_be_same_as_eq_eq), "equals operator overload should be same as = =" };
            yield return new object[] { nameof(Example.X3DX3D_operator_overload_should_be_same_as_equals_method), "== operator overload should be same as equals method" };
            yield return new object[] { nameof(Example.masculine_super_heroes_should_be_buffed), "masculine super heroes should be buffed" };
            yield return new object[] { nameof(Example.termination_date_should_be_updated_when_employee_is_axed), "termination date should be updated when employee is axed" };
            yield return new object[] { nameof(Example.total_amount_should_be_updated_when_order_is_taxed), "total amount should be updated when order is taxed" };
            yield return new object[] { nameof(Example.X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27), "'stuffed' should not be ambiguous with 'st￭'" };
            yield return new object[] { nameof(Example.X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27), "'maxed out' should not be ambiguous with 'maí out'" };
            yield return new object[] { nameof(Example.TestNameShouldRemainUnchanged), "TestNameShouldRemainUnchanged" };
            yield return new object[] { nameof(Example.TestX20NameX20WithU0020Spaces), "Test Name With Spaces" };
        }
    }

    public static IEnumerable<object[]> ReplaceUnderscoreData
    {
        get
        {
            yield return new object[] { nameof(Example.unit_tests_are_awesomeX21_U263A), "unit tests are awesomeX21 U263A" };
            yield return new object[] { nameof(Example.api_version_1_eq_1X2E0), "api version 1 eq 1X2E0" };
            yield return new object[] { nameof(Example.api_version_should_be_greater_than_1), "api version should be greater than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_gt_than_1), "api version 2 should be gt than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_ge_than_1), "api version 2 should be ge than 1" };
            yield return new object[] { nameof(Example.api_version_1_should_be_lt_than_2), "api version 1 should be lt than 2" };
            yield return new object[] { nameof(Example.api_version_1_should_be_le_than_2), "api version 1 should be le than 2" };
            yield return new object[] { nameof(Example.api_version_1X2E0_should_ne_2U002E0), "api version 1X2E0 should ne 2U002E0" };
            yield return new object[] { nameof(Given_a_version_number), "Given a version number" };
            yield return new object[] { nameof(Given_a_version_number.when_it_equals_1X2C_then_it_should_be_less_than_2), "when it equals 1X2C then it should be less than 2" };
            yield return new object[] { nameof(Example.lt_0_should_be_an_error), "lt 0 should be an error" };
            yield return new object[] { nameof(Example.equals_operator_overload_should_be_same_as_eq_eq), "equals operator overload should be same as eq eq" };
            yield return new object[] { nameof(Example.X3DX3D_operator_overload_should_be_same_as_equals_method), "X3DX3D operator overload should be same as equals method" };
            yield return new object[] { nameof(Example.masculine_super_heroes_should_be_buffed), "masculine super heroes should be buffed" };
            yield return new object[] { nameof(Example.termination_date_should_be_updated_when_employee_is_axed), "termination date should be updated when employee is axed" };
            yield return new object[] { nameof(Example.total_amount_should_be_updated_when_order_is_taxed), "total amount should be updated when order is taxed" };
            yield return new object[] { nameof(Example.X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27), "X27stuffedX27 should not be ambiguous with X27stUFFEDX27" };
            yield return new object[] { nameof(Example.X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27), "X27maxed outX27 should not be ambiguous with X27maXED outX27" };
            yield return new object[] { nameof(Example.TestNameShouldRemainUnchanged), "TestNameShouldRemainUnchanged" };
            yield return new object[] { nameof(Example.TestX20NameX20WithU0020Spaces), "TestX20NameX20WithU0020Spaces" };
        }
    }

    public static IEnumerable<object[]> ReplaceUnderscoreAndOperatorData
    {
        get
        {
            yield return new object[] { nameof(Example.unit_tests_are_awesomeX21_U263A), "unit tests are awesomeX21 U263A" };
            yield return new object[] { nameof(Example.api_version_1_eq_1X2E0), "api version 1 = 1X2E0" };
            yield return new object[] { nameof(Example.api_version_should_be_greater_than_1), "api version should be greater than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_gt_than_1), "api version 2 should be > than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_ge_than_1), "api version 2 should be >= than 1" };
            yield return new object[] { nameof(Example.api_version_1_should_be_lt_than_2), "api version 1 should be < than 2" };
            yield return new object[] { nameof(Example.api_version_1_should_be_le_than_2), "api version 1 should be <= than 2" };
            yield return new object[] { nameof(Example.api_version_1X2E0_should_ne_2U002E0), "api version 1X2E0 should != 2U002E0" };
            yield return new object[] { nameof(Given_a_version_number), "Given a version number" };
            yield return new object[] { nameof(Given_a_version_number.when_it_equals_1X2C_then_it_should_be_less_than_2), "when it equals 1X2C then it should be less than 2" };
            yield return new object[] { nameof(Example.lt_0_should_be_an_error), "< 0 should be an error" };
            yield return new object[] { nameof(Example.equals_operator_overload_should_be_same_as_eq_eq), "equals operator overload should be same as = =" };
            yield return new object[] { nameof(Example.X3DX3D_operator_overload_should_be_same_as_equals_method), "X3DX3D operator overload should be same as equals method" };
            yield return new object[] { nameof(Example.masculine_super_heroes_should_be_buffed), "masculine super heroes should be buffed" };
            yield return new object[] { nameof(Example.termination_date_should_be_updated_when_employee_is_axed), "termination date should be updated when employee is axed" };
            yield return new object[] { nameof(Example.total_amount_should_be_updated_when_order_is_taxed), "total amount should be updated when order is taxed" };
            yield return new object[] { nameof(Example.X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27), "X27stuffedX27 should not be ambiguous with X27stUFFEDX27" };
            yield return new object[] { nameof(Example.X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27), "X27maxed outX27 should not be ambiguous with X27maXED outX27" };
            yield return new object[] { nameof(Example.TestNameShouldRemainUnchanged), "TestNameShouldRemainUnchanged" };
            yield return new object[] { nameof(Example.TestX20NameX20WithU0020Spaces), "TestX20NameX20WithU0020Spaces" };
        }
    }

    public static IEnumerable<object[]> ReplaceUnderscoreAndEscapeSequenceData
    {
        get
        {
            yield return new object[] { nameof(Example.unit_tests_are_awesomeX21_U263A), "unit tests are awesome! ☺" };
            yield return new object[] { nameof(Example.api_version_1_eq_1X2E0), "api version 1 eq 1.0" };
            yield return new object[] { nameof(Example.api_version_should_be_greater_than_1), "api version should be greater than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_gt_than_1), "api version 2 should be gt than 1" };
            yield return new object[] { nameof(Example.api_version_2_should_be_ge_than_1), "api version 2 should be ge than 1" };
            yield return new object[] { nameof(Example.api_version_1_should_be_lt_than_2), "api version 1 should be lt than 2" };
            yield return new object[] { nameof(Example.api_version_1_should_be_le_than_2), "api version 1 should be le than 2" };
            yield return new object[] { nameof(Example.api_version_1X2E0_should_ne_2U002E0), "api version 1.0 should ne 2.0" };
            yield return new object[] { nameof(Given_a_version_number), "Given a version number" };
            yield return new object[] { nameof(Given_a_version_number.when_it_equals_1X2C_then_it_should_be_less_than_2), "when it equals 1, then it should be less than 2" };
            yield return new object[] { nameof(Example.lt_0_should_be_an_error), "lt 0 should be an error" };
            yield return new object[] { nameof(Example.equals_operator_overload_should_be_same_as_eq_eq), "equals operator overload should be same as eq eq" };
            yield return new object[] { nameof(Example.X3DX3D_operator_overload_should_be_same_as_equals_method), "== operator overload should be same as equals method" };
            yield return new object[] { nameof(Example.masculine_super_heroes_should_be_buffed), "masculine super heroes should be buffed" };
            yield return new object[] { nameof(Example.termination_date_should_be_updated_when_employee_is_axed), "termination date should be updated when employee is axed" };
            yield return new object[] { nameof(Example.total_amount_should_be_updated_when_order_is_taxed), "total amount should be updated when order is taxed" };
            yield return new object[] { nameof(Example.X27stuffedX27_should_not_be_ambiguous_with_X27stUFFEDX27), "'stuffed' should not be ambiguous with 'st￭'" };
            yield return new object[] { nameof(Example.X27maxed_outX27_should_not_be_ambiguous_with_X27maXED_outX27), "'maxed out' should not be ambiguous with 'maí out'" };
            yield return new object[] { nameof(Example.TestNameShouldRemainUnchanged), "TestNameShouldRemainUnchanged" };
            yield return new object[] { nameof(Example.TestX20NameX20WithU0020Spaces), "Test Name With Spaces" };
        }
    }

    [Theory]
    [MemberData(nameof(UnformattedDisplayNameData))]
    public void FormatShouldReturnExpectedDefaultDisplayName(string name, string expected)
    {
        const TestMethodDisplay display = TestMethodDisplay.ClassAndMethod;
        var formatter = new DisplayNameFormatter(display);
        var actual = formatter.Format(name);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(AllFormatExtensionsData))]
    public void FormatShouldReturnExpectedDisplayNameWithAllFormatExtensions(string name, string expected)
    {
        const TestMethodDisplay display = TestMethodDisplay.ClassAndMethod | TestMethodDisplay.AllFormatExtensions;
        var formatter = new DisplayNameFormatter(display);
        var actual = formatter.Format(name);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ReplaceUnderscoreData))]
    public void FormatShouldReturnExpectedDisplayNameWithSpacesInsteadOfUnderscores(string name, string expected)
    {
        const TestMethodDisplay display = TestMethodDisplay.ClassAndMethod | TestMethodDisplay.ReplaceUnderscoreWithSpace;
        var formatter = new DisplayNameFormatter(display);
        var actual = formatter.Format(name);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ReplaceUnderscoreAndOperatorData))]
    public void FormatShouldReturnExpectedDisplayNameWithReplacedSpacesAndOperators(string name, string expected)
    {
        const TestMethodDisplay display = TestMethodDisplay.ClassAndMethod | TestMethodDisplay.ReplaceUnderscoreWithSpace | TestMethodDisplay.AllowOperatorMonikers;
        var formatter = new DisplayNameFormatter(display);
        var actual = formatter.Format(name);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ReplaceUnderscoreAndEscapeSequenceData))]
    public void FormatShouldReturnExpectedDisplayNameWithReplacedSpacesAndEscapeSequences(string name, string expected)
    {
        const TestMethodDisplay display = TestMethodDisplay.ClassAndMethod | TestMethodDisplay.ReplaceUnderscoreWithSpace | TestMethodDisplay.AllowEscapeSequences;
        var formatter = new DisplayNameFormatter(display);
        var actual = formatter.Format(name);

        Assert.Equal(expected, actual);
    }
}
