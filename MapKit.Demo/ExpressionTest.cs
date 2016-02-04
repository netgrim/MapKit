using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Demo
{
    static class ExpressionTest
    {

        [STAThread]
        static void Main()
        {
            var v = System.Linq.Expressions.Expression.Variable(typeof(int), "yo");
            var call = System.Linq.Expressions.Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }), System.Linq.Expressions.Expression.Constant("pas plus grand"));

            var exp = System.Linq.Expressions.Expression.Block(
                //System.Linq.Expressions.Expression.Assign(v, System.Linq.Expressions.Expression.Constant(42)),
                System.Linq.Expressions.Expression.Condition(
                    System.Linq.Expressions.Expression.GreaterThan(
                    v,
                    System.Linq.Expressions.Expression.Constant(1)),
                    System.Linq.Expressions.Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }), System.Linq.Expressions.Expression.Constant("plus grand que 1")),
                    System.Linq.Expressions.Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }), System.Linq.Expressions.Expression.Constant("pas plus grand que 1")))
                );

            var c = System.Linq.Expressions.Expression.Lambda<Action<int>>(exp, new[] { v }).Compile();
            c(0);
        }
    }
}
