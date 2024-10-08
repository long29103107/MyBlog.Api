﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilteringAndSortingExpression.Directive.Implement
{
    internal class InDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return Constants.ComparisonOperator.In;
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            var function = typeof(ICollection<>).MakeGenericType(property.Type).GetMethod("Contains");

            if (function == null)
                throw new Exception("Contains function is not found");

            return Expression.Call(value, function, property);
        }
    }
}
