//
// FluentExtensions.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//
// (C) 2011 Novell, Inc. (http://www.novell.com)
// (C) 2012 Jb Evain
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

// generated

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mono.Linq.Expressions
{
    public static partial class FluentExtensions
    {
        public static BinaryExpression Assign(this Expression left, Expression right) => Expression.Assign(left, right);

        public static BinaryExpression MakeBinary(this ExpressionType binaryType, Expression left, Expression right)
            => Expression.MakeBinary(binaryType, left, right);

        public static BinaryExpression MakeBinary(this ExpressionType binaryType, Expression left, Expression right,
            bool liftToNull, MethodInfo method)
            => Expression.MakeBinary(binaryType, left, right, liftToNull, method);

        public static BinaryExpression MakeBinary(this ExpressionType binaryType, Expression left, Expression right,
            bool liftToNull, MethodInfo method, LambdaExpression conversion)
            => Expression.MakeBinary(binaryType, left, right, liftToNull, method, conversion);

        public static BinaryExpression Equal(this Expression left, Expression right) => Expression.Equal(left, right);

        public static BinaryExpression
            Equal(this Expression left, Expression right, bool liftToNull, MethodInfo method)
            => Expression.Equal(left, right, liftToNull, method);

        public static BinaryExpression ReferenceEqual(this Expression left, Expression right)
            => Expression.ReferenceEqual(left, right);

        public static BinaryExpression NotEqual(this Expression left, Expression right)
            => Expression.NotEqual(left, right);

        public static BinaryExpression NotEqual(this Expression left, Expression right, bool liftToNull,
            MethodInfo method)
            => Expression.NotEqual(left, right, liftToNull, method);

        public static BinaryExpression ReferenceNotEqual(this Expression left, Expression right)
            => Expression.ReferenceNotEqual(left, right);

        public static BinaryExpression GreaterThan(this Expression left, Expression right)
            => Expression.GreaterThan(left, right);

        public static BinaryExpression GreaterThan(this Expression left, Expression right, bool liftToNull,
            MethodInfo method)
            => Expression.GreaterThan(left, right, liftToNull, method);

        public static BinaryExpression LessThan(this Expression left, Expression right)
            => Expression.LessThan(left, right);

        public static BinaryExpression LessThan(this Expression left, Expression right, bool liftToNull,
            MethodInfo method)
            => Expression.LessThan(left, right, liftToNull, method);

        public static BinaryExpression GreaterThanOrEqual(this Expression left, Expression right)
            => Expression.GreaterThanOrEqual(left, right);

        public static BinaryExpression GreaterThanOrEqual(this Expression left, Expression right, bool liftToNull,
            MethodInfo method)
            => Expression.GreaterThanOrEqual(left, right, liftToNull, method);

        public static BinaryExpression LessThanOrEqual(this Expression left, Expression right)
            => Expression.LessThanOrEqual(left, right);

        public static BinaryExpression LessThanOrEqual(this Expression left, Expression right, bool liftToNull,
            MethodInfo method)
            => Expression.LessThanOrEqual(left, right, liftToNull, method);

        public static BinaryExpression AndAlso(this Expression left, Expression right)
            => Expression.AndAlso(left, right);

        public static BinaryExpression AndAlso(this Expression left, Expression right, MethodInfo method)
            => Expression.AndAlso(left, right, method);

        public static BinaryExpression OrElse(this Expression left, Expression right) => Expression.OrElse(left, right);

        public static BinaryExpression OrElse(this Expression left, Expression right, MethodInfo method)
            => Expression.OrElse(left, right, method);

        public static BinaryExpression Coalesce(this Expression left, Expression right)
            => Expression.Coalesce(left, right);

        public static BinaryExpression Coalesce(this Expression left, Expression right, LambdaExpression conversion)
            => Expression.Coalesce(left, right, conversion);

        public static BinaryExpression Add(this Expression left, Expression right) => Expression.Add(left, right);

        public static BinaryExpression Add(this Expression left, Expression right, MethodInfo method)
            => Expression.Add(left, right, method);

        public static BinaryExpression AddAssign(this Expression left, Expression right)
            => Expression.AddAssign(left, right);

        public static BinaryExpression AddAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.AddAssign(left, right, method);

        public static BinaryExpression AddAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.AddAssign(left, right, method, conversion);

        public static BinaryExpression AddAssignChecked(this Expression left, Expression right)
            => Expression.AddAssignChecked(left, right);

        public static BinaryExpression AddAssignChecked(this Expression left, Expression right, MethodInfo method)
            => Expression.AddAssignChecked(left, right, method);

        public static BinaryExpression AddAssignChecked(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.AddAssignChecked(left, right, method, conversion);

        public static BinaryExpression AddChecked(this Expression left, Expression right)
            => Expression.AddChecked(left, right);

        public static BinaryExpression AddChecked(this Expression left, Expression right, MethodInfo method)
            => Expression.AddChecked(left, right, method);

        public static BinaryExpression Subtract(this Expression left, Expression right)
            => Expression.Subtract(left, right);

        public static BinaryExpression Subtract(this Expression left, Expression right, MethodInfo method)
            => Expression.Subtract(left, right, method);

        public static BinaryExpression SubtractAssign(this Expression left, Expression right)
            => Expression.SubtractAssign(left, right);

        public static BinaryExpression SubtractAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.SubtractAssign(left, right, method);

        public static BinaryExpression SubtractAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.SubtractAssign(left, right, method, conversion);

        public static BinaryExpression SubtractAssignChecked(this Expression left, Expression right)
            => Expression.SubtractAssignChecked(left, right);

        public static BinaryExpression
            SubtractAssignChecked(this Expression left, Expression right, MethodInfo method)
            => Expression.SubtractAssignChecked(left, right, method);

        public static BinaryExpression SubtractAssignChecked(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.SubtractAssignChecked(left, right, method, conversion);

        public static BinaryExpression SubtractChecked(this Expression left, Expression right)
            => Expression.SubtractChecked(left, right);

        public static BinaryExpression SubtractChecked(this Expression left, Expression right, MethodInfo method)
            => Expression.SubtractChecked(left, right, method);

        public static BinaryExpression Divide(this Expression left, Expression right) => Expression.Divide(left, right);

        public static BinaryExpression Divide(this Expression left, Expression right, MethodInfo method)
            => Expression.Divide(left, right, method);

        public static BinaryExpression DivideAssign(this Expression left, Expression right)
            => Expression.DivideAssign(left, right);

        public static BinaryExpression DivideAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.DivideAssign(left, right, method);

        public static BinaryExpression DivideAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.DivideAssign(left, right, method, conversion);

        public static BinaryExpression Modulo(this Expression left, Expression right) => Expression.Modulo(left, right);

        public static BinaryExpression Modulo(this Expression left, Expression right, MethodInfo method)
            => Expression.Modulo(left, right, method);

        public static BinaryExpression ModuloAssign(this Expression left, Expression right)
            => Expression.ModuloAssign(left, right);

        public static BinaryExpression ModuloAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.ModuloAssign(left, right, method);

        public static BinaryExpression ModuloAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.ModuloAssign(left, right, method, conversion);

        public static BinaryExpression Multiply(this Expression left, Expression right)
            => Expression.Multiply(left, right);

        public static BinaryExpression Multiply(this Expression left, Expression right, MethodInfo method)
            => Expression.Multiply(left, right, method);

        public static BinaryExpression MultiplyAssign(this Expression left, Expression right)
            => Expression.MultiplyAssign(left, right);

        public static BinaryExpression MultiplyAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.MultiplyAssign(left, right, method);

        public static BinaryExpression MultiplyAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.MultiplyAssign(left, right, method, conversion);

        public static BinaryExpression MultiplyAssignChecked(this Expression left, Expression right)
            => Expression.MultiplyAssignChecked(left, right);

        public static BinaryExpression
            MultiplyAssignChecked(this Expression left, Expression right, MethodInfo method)
            => Expression.MultiplyAssignChecked(left, right, method);

        public static BinaryExpression MultiplyAssignChecked(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.MultiplyAssignChecked(left, right, method, conversion);

        public static BinaryExpression MultiplyChecked(this Expression left, Expression right)
            => Expression.MultiplyChecked(left, right);

        public static BinaryExpression MultiplyChecked(this Expression left, Expression right, MethodInfo method)
            => Expression.MultiplyChecked(left, right, method);

        public static BinaryExpression LeftShift(this Expression left, Expression right)
            => Expression.LeftShift(left, right);

        public static BinaryExpression LeftShift(this Expression left, Expression right, MethodInfo method)
            => Expression.LeftShift(left, right, method);

        public static BinaryExpression LeftShiftAssign(this Expression left, Expression right)
            => Expression.LeftShiftAssign(left, right);

        public static BinaryExpression LeftShiftAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.LeftShiftAssign(left, right, method);

        public static BinaryExpression LeftShiftAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.LeftShiftAssign(left, right, method, conversion);

        public static BinaryExpression RightShift(this Expression left, Expression right)
            => Expression.RightShift(left, right);

        public static BinaryExpression RightShift(this Expression left, Expression right, MethodInfo method)
            => Expression.RightShift(left, right, method);

        public static BinaryExpression RightShiftAssign(this Expression left, Expression right)
            => Expression.RightShiftAssign(left, right);

        public static BinaryExpression RightShiftAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.RightShiftAssign(left, right, method);

        public static BinaryExpression RightShiftAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.RightShiftAssign(left, right, method, conversion);

        public static BinaryExpression And(this Expression left, Expression right) => Expression.And(left, right);

        public static BinaryExpression And(this Expression left, Expression right, MethodInfo method)
            => Expression.And(left, right, method);

        public static BinaryExpression AndAssign(this Expression left, Expression right)
            => Expression.AndAssign(left, right);

        public static BinaryExpression AndAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.AndAssign(left, right, method);

        public static BinaryExpression AndAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.AndAssign(left, right, method, conversion);

        public static BinaryExpression Or(this Expression left, Expression right) => Expression.Or(left, right);

        public static BinaryExpression Or(this Expression left, Expression right, MethodInfo method)
            => Expression.Or(left, right, method);

        public static BinaryExpression OrAssign(this Expression left, Expression right)
            => Expression.OrAssign(left, right);

        public static BinaryExpression OrAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.OrAssign(left, right, method);

        public static BinaryExpression OrAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.OrAssign(left, right, method, conversion);

        public static BinaryExpression ExclusiveOr(this Expression left, Expression right)
            => Expression.ExclusiveOr(left, right);

        public static BinaryExpression ExclusiveOr(this Expression left, Expression right, MethodInfo method)
            => Expression.ExclusiveOr(left, right, method);

        public static BinaryExpression ExclusiveOrAssign(this Expression left, Expression right)
            => Expression.ExclusiveOrAssign(left, right);

        public static BinaryExpression ExclusiveOrAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.ExclusiveOrAssign(left, right, method);

        public static BinaryExpression ExclusiveOrAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.ExclusiveOrAssign(left, right, method, conversion);

        public static BinaryExpression Power(this Expression left, Expression right) => Expression.Power(left, right);

        public static BinaryExpression Power(this Expression left, Expression right, MethodInfo method)
            => Expression.Power(left, right, method);

        public static BinaryExpression PowerAssign(this Expression left, Expression right)
            => Expression.PowerAssign(left, right);

        public static BinaryExpression PowerAssign(this Expression left, Expression right, MethodInfo method)
            => Expression.PowerAssign(left, right, method);

        public static BinaryExpression PowerAssign(this Expression left, Expression right, MethodInfo method,
            LambdaExpression conversion)
            => Expression.PowerAssign(left, right, method, conversion);

        public static BinaryExpression ArrayIndex(this Expression array, Expression index)
            => Expression.ArrayIndex(array, index);

        public static BlockExpression Block(this Expression arg0, Expression arg1) => Expression.Block(arg0, arg1);

        public static BlockExpression Block(this Expression arg0, Expression arg1, Expression arg2)
            => Expression.Block(arg0, arg1, arg2);

        public static BlockExpression Block(this Expression arg0, Expression arg1, Expression arg2, Expression arg3)
            => Expression.Block(arg0, arg1, arg2, arg3);

        public static BlockExpression Block(this Expression arg0, Expression arg1, Expression arg2, Expression arg3,
            Expression arg4)
            => Expression.Block(arg0, arg1, arg2, arg3, arg4);

        public static BlockExpression Block(this IEnumerable<Expression> expressions) => Expression.Block(expressions);

        public static BlockExpression Block(this Type type, params Expression[] expressions)
            => Expression.Block(type, expressions);

        public static BlockExpression Block(this Type type, IEnumerable<Expression> expressions)
            => Expression.Block(type, expressions);

        public static BlockExpression Block(this IEnumerable<ParameterExpression> variables,
            params Expression[] expressions)
            => Expression.Block(variables, expressions);

        public static BlockExpression Block(this Type type, IEnumerable<ParameterExpression> variables,
            params Expression[] expressions)
            => Expression.Block(type, variables, expressions);

        public static BlockExpression Block(this IEnumerable<ParameterExpression> variables,
            IEnumerable<Expression> expressions)
            => Expression.Block(variables, expressions);

        public static BlockExpression Block(this Type type, IEnumerable<ParameterExpression> variables,
            IEnumerable<Expression> expressions)
            => Expression.Block(type, variables, expressions);

        public static CatchBlock Catch(this Type type, Expression body) => Expression.Catch(type, body);

        public static CatchBlock Catch(this ParameterExpression variable, Expression body)
            => Expression.Catch(variable, body);

        public static CatchBlock Catch(this Type type, Expression body, Expression filter)
            => Expression.Catch(type, body, filter);

        public static CatchBlock Catch(this ParameterExpression variable, Expression body, Expression filter)
            => Expression.Catch(variable, body, filter);

        public static CatchBlock MakeCatchBlock(this Type type, ParameterExpression variable, Expression body,
            Expression filter)
            => Expression.MakeCatchBlock(type, variable, body, filter);

        public static ConditionalExpression Condition(this Expression test, Expression ifTrue, Expression ifFalse)
            => Expression.Condition(test, ifTrue, ifFalse);

        public static ConditionalExpression Condition(this Expression test, Expression ifTrue, Expression ifFalse,
            Type type)
            => Expression.Condition(test, ifTrue, ifFalse, type);

        public static ConditionalExpression IfThen(this Expression test, Expression ifTrue)
            => Expression.IfThen(test, ifTrue);

        public static ConditionalExpression IfThenElse(this Expression test, Expression ifTrue, Expression ifFalse)
            => Expression.IfThenElse(test, ifTrue, ifFalse);

        public static ConstantExpression Constant(this object value) => Expression.Constant(value);

        public static ConstantExpression Constant(this object value, Type type) => Expression.Constant(value, type);

        public static DebugInfoExpression DebugInfo(this SymbolDocumentInfo document, int startLine, int startColumn,
            int endLine, int endColumn)
            => Expression.DebugInfo(document, startLine, startColumn, endLine, endColumn);

        public static DebugInfoExpression ClearDebugInfo(this SymbolDocumentInfo document)
            => Expression.ClearDebugInfo(document);

        public static DefaultExpression Default(this Type type) => Expression.Default(type);

        public static DynamicExpression MakeDynamic(this Type delegateType, CallSiteBinder binder,
            params Expression[] arguments)
            => Expression.MakeDynamic(delegateType, binder, arguments);

        public static DynamicExpression MakeDynamic(this Type delegateType, CallSiteBinder binder,
            IEnumerable<Expression> arguments)
            => Expression.MakeDynamic(delegateType, binder, arguments);

        public static DynamicExpression MakeDynamic(this Type delegateType, CallSiteBinder binder, Expression arg0)
            => Expression.MakeDynamic(delegateType, binder, arg0);

        public static DynamicExpression MakeDynamic(this Type delegateType, CallSiteBinder binder, Expression arg0,
            Expression arg1)
            => Expression.MakeDynamic(delegateType, binder, arg0, arg1);

        public static DynamicExpression MakeDynamic(this Type delegateType, CallSiteBinder binder, Expression arg0,
            Expression arg1, Expression arg2)
            => Expression.MakeDynamic(delegateType, binder, arg0, arg1, arg2);

        public static DynamicExpression MakeDynamic(this Type delegateType, CallSiteBinder binder, Expression arg0,
            Expression arg1, Expression arg2, Expression arg3)
            => Expression.MakeDynamic(delegateType, binder, arg0, arg1, arg2, arg3);

        public static DynamicExpression Dynamic(this CallSiteBinder binder, Type returnType,
            params Expression[] arguments)
            => Expression.Dynamic(binder, returnType, arguments);

        public static DynamicExpression Dynamic(this CallSiteBinder binder, Type returnType, Expression arg0)
            => Expression.Dynamic(binder, returnType, arg0);

        public static DynamicExpression Dynamic(this CallSiteBinder binder, Type returnType, Expression arg0,
            Expression arg1)
            => Expression.Dynamic(binder, returnType, arg0, arg1);

        public static DynamicExpression Dynamic(this CallSiteBinder binder, Type returnType, Expression arg0,
            Expression arg1, Expression arg2)
            => Expression.Dynamic(binder, returnType, arg0, arg1, arg2);

        public static DynamicExpression Dynamic(this CallSiteBinder binder, Type returnType, Expression arg0,
            Expression arg1, Expression arg2, Expression arg3)
            => Expression.Dynamic(binder, returnType, arg0, arg1, arg2, arg3);

        public static DynamicExpression Dynamic(this CallSiteBinder binder, Type returnType,
            IEnumerable<Expression> arguments)
            => Expression.Dynamic(binder, returnType, arguments);

        public static ElementInit ElementInit(this MethodInfo addMethod, params Expression[] arguments)
            => Expression.ElementInit(addMethod, arguments);

        public static ElementInit ElementInit(this MethodInfo addMethod, IEnumerable<Expression> arguments)
            => Expression.ElementInit(addMethod, arguments);

        public static GotoExpression Break(this LabelTarget target) => Expression.Break(target);

        public static GotoExpression Break(this LabelTarget target, Expression value)
            => Expression.Break(target, value);

        public static GotoExpression Break(this LabelTarget target, Type type) => Expression.Break(target, type);

        public static GotoExpression Break(this LabelTarget target, Expression value, Type type)
            => Expression.Break(target, value, type);

        public static GotoExpression Continue(this LabelTarget target) => Expression.Continue(target);

        public static GotoExpression Continue(this LabelTarget target, Type type) => Expression.Continue(target, type);

        public static GotoExpression Return(this LabelTarget target) => Expression.Return(target);

        public static GotoExpression Return(this LabelTarget target, Type type) => Expression.Return(target, type);

        public static GotoExpression Return(this LabelTarget target, Expression value)
            => Expression.Return(target, value);

        public static GotoExpression Return(this LabelTarget target, Expression value, Type type)
            => Expression.Return(target, value, type);

        public static GotoExpression Goto(this LabelTarget target) => Expression.Goto(target);

        public static GotoExpression Goto(this LabelTarget target, Type type) => Expression.Goto(target, type);

        public static GotoExpression Goto(this LabelTarget target, Expression value) => Expression.Goto(target, value);

        public static GotoExpression Goto(this LabelTarget target, Expression value, Type type)
            => Expression.Goto(target, value, type);

        public static GotoExpression MakeGoto(this GotoExpressionKind kind, LabelTarget target, Expression value,
            Type type)
            => Expression.MakeGoto(kind, target, value, type);

        public static IndexExpression MakeIndex(this Expression instance, PropertyInfo indexer,
            IEnumerable<Expression> arguments)
            => Expression.MakeIndex(instance, indexer, arguments);

        public static IndexExpression ArrayAccess(this Expression array, params Expression[] indexes)
            => Expression.ArrayAccess(array, indexes);

        public static IndexExpression ArrayAccess(this Expression array, IEnumerable<Expression> indexes)
            => Expression.ArrayAccess(array, indexes);

        public static IndexExpression Property(this Expression instance, string propertyName,
            params Expression[] arguments)
            => Expression.Property(instance, propertyName, arguments);

        public static IndexExpression Property(this Expression instance, PropertyInfo indexer,
            params Expression[] arguments)
            => Expression.Property(instance, indexer, arguments);

        public static IndexExpression Property(this Expression instance, PropertyInfo indexer,
            IEnumerable<Expression> arguments)
            => Expression.Property(instance, indexer, arguments);

        public static InvocationExpression Invoke(this Expression expression, params Expression[] arguments)
            => Expression.Invoke(expression, arguments);

        public static InvocationExpression Invoke(this Expression expression, IEnumerable<Expression> arguments)
            => Expression.Invoke(expression, arguments);

        public static LabelExpression Label(this LabelTarget target) => Expression.Label(target);

        public static LabelExpression Label(this LabelTarget target, Expression defaultValue)
            => Expression.Label(target, defaultValue);

        public static LabelTarget Label(this string name) => Expression.Label(name);

        public static LabelTarget Label(this Type type) => Expression.Label(type);

        public static LabelTarget Label(this Type type, string name) => Expression.Label(type, name);

        public static Expression<TDelegate> Lambda<TDelegate>(this Expression body,
            params ParameterExpression[] parameters)
            => Expression.Lambda<TDelegate>(body, parameters);

        public static Expression<TDelegate> Lambda<TDelegate>(this Expression body, bool tailCall,
            params ParameterExpression[] parameters)
            => Expression.Lambda<TDelegate>(body, tailCall, parameters);

        public static Expression<TDelegate> Lambda<TDelegate>(this Expression body,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda<TDelegate>(body, parameters);

        public static Expression<TDelegate> Lambda<TDelegate>(this Expression body, bool tailCall,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda<TDelegate>(body, tailCall, parameters);

        public static Expression<TDelegate> Lambda<TDelegate>(this Expression body, string name,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda<TDelegate>(body, name, parameters);

        public static Expression<TDelegate> Lambda<TDelegate>(this Expression body, string name, bool tailCall,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda<TDelegate>(body, name, tailCall, parameters);

        public static LambdaExpression Lambda(this Expression body, params ParameterExpression[] parameters)
            => Expression.Lambda(body, parameters);

        public static LambdaExpression Lambda(this Expression body, bool tailCall,
            params ParameterExpression[] parameters)
            => Expression.Lambda(body, tailCall, parameters);

        public static LambdaExpression Lambda(this Expression body, IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(body, parameters);

        public static LambdaExpression Lambda(this Expression body, bool tailCall,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(body, tailCall, parameters);

        public static LambdaExpression Lambda(this Type delegateType, Expression body,
            params ParameterExpression[] parameters)
            => Expression.Lambda(delegateType, body, parameters);

        public static LambdaExpression Lambda(this Type delegateType, Expression body, bool tailCall,
            params ParameterExpression[] parameters)
            => Expression.Lambda(delegateType, body, tailCall, parameters);

        public static LambdaExpression Lambda(this Type delegateType, Expression body,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(delegateType, body, parameters);

        public static LambdaExpression Lambda(this Type delegateType, Expression body, bool tailCall,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(delegateType, body, tailCall, parameters);

        public static LambdaExpression Lambda(this Expression body, string name,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(body, name, parameters);

        public static LambdaExpression Lambda(this Expression body, string name, bool tailCall,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(body, name, tailCall, parameters);

        public static LambdaExpression Lambda(this Type delegateType, Expression body, string name,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(delegateType, body, name, parameters);

        public static LambdaExpression Lambda(this Type delegateType, Expression body, string name, bool tailCall,
            IEnumerable<ParameterExpression> parameters)
            => Expression.Lambda(delegateType, body, name, tailCall, parameters);

        public static ListInitExpression ListInit(this NewExpression newExpression, params Expression[] initializers)
            => Expression.ListInit(newExpression, initializers);

        public static ListInitExpression ListInit(this NewExpression newExpression,
            IEnumerable<Expression> initializers)
            => Expression.ListInit(newExpression, initializers);

        public static ListInitExpression ListInit(this NewExpression newExpression, MethodInfo addMethod,
            params Expression[] initializers)
            => Expression.ListInit(newExpression, addMethod, initializers);

        public static ListInitExpression ListInit(this NewExpression newExpression, MethodInfo addMethod,
            IEnumerable<Expression> initializers)
            => Expression.ListInit(newExpression, addMethod, initializers);

        public static ListInitExpression
            ListInit(this NewExpression newExpression, params ElementInit[] initializers)
            => Expression.ListInit(newExpression, initializers);

        public static ListInitExpression ListInit(this NewExpression newExpression,
            IEnumerable<ElementInit> initializers)
            => Expression.ListInit(newExpression, initializers);

        public static LoopExpression Loop(this Expression body) => Expression.Loop(body);

        public static LoopExpression Loop(this Expression body, LabelTarget @break) => Expression.Loop(body, @break);

        public static LoopExpression Loop(this Expression body, LabelTarget @break, LabelTarget @continue)
            => Expression.Loop(body, @break, @continue);

        public static MemberAssignment Bind(this MemberInfo member, Expression expression)
            => Expression.Bind(member, expression);

        public static MemberAssignment Bind(this MethodInfo propertyAccessor, Expression expression)
            => Expression.Bind(propertyAccessor, expression);

        public static MemberExpression Field(this Expression expression, FieldInfo field)
            => Expression.Field(expression, field);

        public static MemberExpression Field(this Expression expression, string fieldName)
            => Expression.Field(expression, fieldName);

        public static MemberExpression Field(this Expression expression, Type type, string fieldName)
            => Expression.Field(expression, type, fieldName);

        public static MemberExpression Property(this Expression expression, string propertyName)
            => Expression.Property(expression, propertyName);

        public static MemberExpression Property(this Expression expression, Type type, string propertyName)
            => Expression.Property(expression, type, propertyName);

        public static MemberExpression Property(this Expression expression, PropertyInfo property)
            => Expression.Property(expression, property);

        public static MemberExpression Property(this Expression expression, MethodInfo propertyAccessor)
            => Expression.Property(expression, propertyAccessor);

        public static MemberExpression PropertyOrField(this Expression expression, string propertyOrFieldName)
            => Expression.PropertyOrField(expression, propertyOrFieldName);

        public static MemberExpression MakeMemberAccess(this Expression expression, MemberInfo member)
            => Expression.MakeMemberAccess(expression, member);

        public static MemberInitExpression
            MemberInit(this NewExpression newExpression, params MemberBinding[] bindings)
            => Expression.MemberInit(newExpression, bindings);

        public static MemberInitExpression MemberInit(this NewExpression newExpression,
            IEnumerable<MemberBinding> bindings)
            => Expression.MemberInit(newExpression, bindings);

        public static MemberListBinding ListBind(this MemberInfo member, params ElementInit[] initializers)
            => Expression.ListBind(member, initializers);

        public static MemberListBinding ListBind(this MemberInfo member, IEnumerable<ElementInit> initializers)
            => Expression.ListBind(member, initializers);

        public static MemberListBinding ListBind(this MethodInfo propertyAccessor, params ElementInit[] initializers)
            => Expression.ListBind(propertyAccessor, initializers);

        public static MemberListBinding ListBind(this MethodInfo propertyAccessor,
            IEnumerable<ElementInit> initializers)
            => Expression.ListBind(propertyAccessor, initializers);

        public static MemberMemberBinding MemberBind(this MemberInfo member, params MemberBinding[] bindings)
            => Expression.MemberBind(member, bindings);

        public static MemberMemberBinding MemberBind(this MemberInfo member, IEnumerable<MemberBinding> bindings)
            => Expression.MemberBind(member, bindings);

        public static MemberMemberBinding
            MemberBind(this MethodInfo propertyAccessor, params MemberBinding[] bindings)
            => Expression.MemberBind(propertyAccessor, bindings);

        public static MemberMemberBinding MemberBind(this MethodInfo propertyAccessor,
            IEnumerable<MemberBinding> bindings)
            => Expression.MemberBind(propertyAccessor, bindings);

        public static MethodCallExpression Call(this MethodInfo method, Expression arg0)
            => Expression.Call(method, arg0);

        public static MethodCallExpression Call(this MethodInfo method, Expression arg0, Expression arg1)
            => Expression.Call(method, arg0, arg1);

        public static MethodCallExpression Call(this MethodInfo method, Expression arg0, Expression arg1,
            Expression arg2)
            => Expression.Call(method, arg0, arg1, arg2);

        public static MethodCallExpression Call(this MethodInfo method, Expression arg0, Expression arg1,
            Expression arg2, Expression arg3)
            => Expression.Call(method, arg0, arg1, arg2, arg3);

        public static MethodCallExpression Call(this MethodInfo method, Expression arg0, Expression arg1,
            Expression arg2, Expression arg3, Expression arg4)
            => Expression.Call(method, arg0, arg1, arg2, arg3, arg4);

        public static MethodCallExpression Call(this MethodInfo method, params Expression[] arguments)
            => Expression.Call(method, arguments);

        public static MethodCallExpression Call(this MethodInfo method, IEnumerable<Expression> arguments)
            => Expression.Call(method, arguments);

        public static MethodCallExpression Call(this Expression instance, MethodInfo method)
            => Expression.Call(instance, method);

        public static MethodCallExpression Call(this Expression instance, MethodInfo method,
            params Expression[] arguments)
            => Expression.Call(instance, method, arguments);

        public static MethodCallExpression Call(this Expression instance, MethodInfo method, Expression arg0,
            Expression arg1)
            => Expression.Call(instance, method, arg0, arg1);

        public static MethodCallExpression Call(this Expression instance, MethodInfo method, Expression arg0,
            Expression arg1, Expression arg2)
            => Expression.Call(instance, method, arg0, arg1, arg2);

        public static MethodCallExpression Call(this Expression instance, string methodName, Type[] typeArguments,
            params Expression[] arguments)
            => Expression.Call(instance, methodName, typeArguments, arguments);

        public static MethodCallExpression Call(this Type type, string methodName, Type[] typeArguments,
            params Expression[] arguments)
            => Expression.Call(type, methodName, typeArguments, arguments);

        public static MethodCallExpression Call(this Expression instance, MethodInfo method,
            IEnumerable<Expression> arguments)
            => Expression.Call(instance, method, arguments);

        public static MethodCallExpression ArrayIndex(this Expression array, params Expression[] indexes)
            => Expression.ArrayIndex(array, indexes);

        public static MethodCallExpression ArrayIndex(this Expression array, IEnumerable<Expression> indexes)
            => Expression.ArrayIndex(array, indexes);

        public static NewArrayExpression NewArrayInit(this Type type, params Expression[] initializers)
            => Expression.NewArrayInit(type, initializers);

        public static NewArrayExpression NewArrayInit(this Type type, IEnumerable<Expression> initializers)
            => Expression.NewArrayInit(type, initializers);

        public static NewArrayExpression NewArrayBounds(this Type type, params Expression[] bounds)
            => Expression.NewArrayBounds(type, bounds);

        public static NewArrayExpression NewArrayBounds(this Type type, IEnumerable<Expression> bounds)
            => Expression.NewArrayBounds(type, bounds);

        public static NewExpression New(this ConstructorInfo constructor) => Expression.New(constructor);

        public static NewExpression New(this ConstructorInfo constructor, params Expression[] arguments)
            => Expression.New(constructor, arguments);

        public static NewExpression New(this ConstructorInfo constructor, IEnumerable<Expression> arguments)
            => Expression.New(constructor, arguments);

        public static NewExpression New(this ConstructorInfo constructor, IEnumerable<Expression> arguments,
            IEnumerable<MemberInfo> members)
            => Expression.New(constructor, arguments, members);

        public static NewExpression New(this ConstructorInfo constructor, IEnumerable<Expression> arguments,
            params MemberInfo[] members)
            => Expression.New(constructor, arguments, members);

        public static NewExpression New(this Type type) => Expression.New(type);

        public static ParameterExpression Parameter(this Type type) => Expression.Parameter(type);

        public static ParameterExpression Variable(this Type type) => Expression.Variable(type);

        public static ParameterExpression Parameter(this Type type, string name) => Expression.Parameter(type, name);

        public static ParameterExpression Variable(this Type type, string name) => Expression.Variable(type, name);

        public static RuntimeVariablesExpression RuntimeVariables(this IEnumerable<ParameterExpression> variables)
            => Expression.RuntimeVariables(variables);

        public static SwitchCase SwitchCase(this Expression body, params Expression[] testValues)
            => Expression.SwitchCase(body, testValues);

        public static SwitchCase SwitchCase(this Expression body, IEnumerable<Expression> testValues)
            => Expression.SwitchCase(body, testValues);

        public static SwitchExpression Switch(this Expression switchValue, params SwitchCase[] cases)
            => Expression.Switch(switchValue, cases);

        public static SwitchExpression Switch(this Expression switchValue, Expression defaultBody,
            params SwitchCase[] cases)
            => Expression.Switch(switchValue, defaultBody, cases);

        public static SwitchExpression Switch(this Expression switchValue, Expression defaultBody,
            MethodInfo comparison, params SwitchCase[] cases)
            => Expression.Switch(switchValue, defaultBody, comparison, cases);

        public static SwitchExpression Switch(this Type type, Expression switchValue, Expression defaultBody,
            MethodInfo comparison, params SwitchCase[] cases)
            => Expression.Switch(type, switchValue, defaultBody, comparison, cases);

        public static SwitchExpression Switch(this Expression switchValue, Expression defaultBody,
            MethodInfo comparison, IEnumerable<SwitchCase> cases)
            => Expression.Switch(switchValue, defaultBody, comparison, cases);

        public static SwitchExpression Switch(this Type type, Expression switchValue, Expression defaultBody,
            MethodInfo comparison, IEnumerable<SwitchCase> cases)
            => Expression.Switch(type, switchValue, defaultBody, comparison, cases);

        public static SymbolDocumentInfo SymbolDocument(this string fileName) => Expression.SymbolDocument(fileName);

        public static SymbolDocumentInfo SymbolDocument(this string fileName, Guid language)
            => Expression.SymbolDocument(fileName, language);

        public static SymbolDocumentInfo SymbolDocument(this string fileName, Guid language, Guid languageVendor)
            => Expression.SymbolDocument(fileName, language, languageVendor);

        public static SymbolDocumentInfo SymbolDocument(this string fileName, Guid language, Guid languageVendor,
            Guid documentType)
            => Expression.SymbolDocument(fileName, language, languageVendor, documentType);

        public static TryExpression MakeTry(this Type type, Expression body, Expression @finally, Expression fault,
            IEnumerable<CatchBlock> handlers)
            => Expression.MakeTry(type, body, @finally, fault, handlers);

        public static TypeBinaryExpression TypeIs(this Expression expression, Type type)
            => Expression.TypeIs(expression, type);

        public static TypeBinaryExpression TypeEqual(this Expression expression, Type type)
            => Expression.TypeEqual(expression, type);

        public static UnaryExpression MakeUnary(this ExpressionType unaryType, Expression operand, Type type)
            => Expression.MakeUnary(unaryType, operand, type);

        public static UnaryExpression MakeUnary(this ExpressionType unaryType, Expression operand, Type type,
            MethodInfo method)
            => Expression.MakeUnary(unaryType, operand, type, method);

        public static UnaryExpression Negate(this Expression expression) => Expression.Negate(expression);

        public static UnaryExpression Negate(this Expression expression, MethodInfo method)
            => Expression.Negate(expression, method);

        public static UnaryExpression UnaryPlus(this Expression expression) => Expression.UnaryPlus(expression);

        public static UnaryExpression UnaryPlus(this Expression expression, MethodInfo method)
            => Expression.UnaryPlus(expression, method);

        public static UnaryExpression NegateChecked(this Expression expression) => Expression.NegateChecked(expression);

        public static UnaryExpression NegateChecked(this Expression expression, MethodInfo method)
            => Expression.NegateChecked(expression, method);

        public static UnaryExpression Not(this Expression expression) => Expression.Not(expression);

        public static UnaryExpression Not(this Expression expression, MethodInfo method)
            => Expression.Not(expression, method);

        public static UnaryExpression IsFalse(this Expression expression) => Expression.IsFalse(expression);

        public static UnaryExpression IsFalse(this Expression expression, MethodInfo method)
            => Expression.IsFalse(expression, method);

        public static UnaryExpression IsTrue(this Expression expression) => Expression.IsTrue(expression);

        public static UnaryExpression IsTrue(this Expression expression, MethodInfo method)
            => Expression.IsTrue(expression, method);

        public static UnaryExpression OnesComplement(this Expression expression)
            => Expression.OnesComplement(expression);

        public static UnaryExpression OnesComplement(this Expression expression, MethodInfo method)
            => Expression.OnesComplement(expression, method);

        public static UnaryExpression TypeAs(this Expression expression, Type type)
            => Expression.TypeAs(expression, type);

        public static UnaryExpression Unbox(this Expression expression, Type type)
            => Expression.Unbox(expression, type);

        public static UnaryExpression Convert(this Expression expression, Type type)
            => Expression.Convert(expression, type);

        public static UnaryExpression Convert(this Expression expression, Type type, MethodInfo method)
            => Expression.Convert(expression, type, method);

        public static UnaryExpression ConvertChecked(this Expression expression, Type type)
            => Expression.ConvertChecked(expression, type);

        public static UnaryExpression ConvertChecked(this Expression expression, Type type, MethodInfo method)
            => Expression.ConvertChecked(expression, type, method);

        public static UnaryExpression ArrayLength(this Expression array) => Expression.ArrayLength(array);

        public static UnaryExpression Quote(this Expression expression) => Expression.Quote(expression);

        public static UnaryExpression Rethrow(this Type type) => Expression.Rethrow(type);

        public static UnaryExpression Throw(this Expression value) => Expression.Throw(value);

        public static UnaryExpression Throw(this Expression value, Type type) => Expression.Throw(value, type);

        public static UnaryExpression Increment(this Expression expression) => Expression.Increment(expression);

        public static UnaryExpression Increment(this Expression expression, MethodInfo method)
            => Expression.Increment(expression, method);

        public static UnaryExpression Decrement(this Expression expression) => Expression.Decrement(expression);

        public static UnaryExpression Decrement(this Expression expression, MethodInfo method)
            => Expression.Decrement(expression, method);

        public static UnaryExpression PreIncrementAssign(this Expression expression)
            => Expression.PreIncrementAssign(expression);

        public static UnaryExpression PreIncrementAssign(this Expression expression, MethodInfo method)
            => Expression.PreIncrementAssign(expression, method);

        public static UnaryExpression PreDecrementAssign(this Expression expression)
            => Expression.PreDecrementAssign(expression);

        public static UnaryExpression PreDecrementAssign(this Expression expression, MethodInfo method)
            => Expression.PreDecrementAssign(expression, method);

        public static UnaryExpression PostIncrementAssign(this Expression expression)
            => Expression.PostIncrementAssign(expression);

        public static UnaryExpression PostIncrementAssign(this Expression expression, MethodInfo method)
            => Expression.PostIncrementAssign(expression, method);

        public static UnaryExpression PostDecrementAssign(this Expression expression)
            => Expression.PostDecrementAssign(expression);

        public static UnaryExpression PostDecrementAssign(this Expression expression, MethodInfo method)
            => Expression.PostDecrementAssign(expression, method);
    }
}