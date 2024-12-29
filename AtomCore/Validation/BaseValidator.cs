using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AtomCore.Validation;

public class BaseValidator<T> : AbstractValidator<T>
{
    protected bool BePhoneNumber(string text)
    {
        var isMatch = Regex.IsMatch(text, @"^\+[1-9]\d{1,14}$");
        return isMatch;
    }

    protected bool ContainAtLeastOneNumberCharacter(string text)
    {
        foreach (var ch in text)
            if (ch >= 48 && ch <= 57)
                return true;

        return false;
    }
}
