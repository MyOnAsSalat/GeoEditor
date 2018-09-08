using System;
using NCalc;
using UnityEngine;

public class Function
{
    private readonly Expression func;

    public Function(string userFunction)
    {
        func = new Expression(userFunction);
        addMathFunction(func);
    }


    public float GetValue(double x)
    {
        func.Parameters["x"] = x;
        return float.Parse(func.Evaluate().ToString());
    }

    private void addMathFunction(Expression e)
    {
        e.EvaluateFunction += delegate(string name, FunctionArgs args)
        {
            if (name == "sin")
                args.Result = Mathf.Sin(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "cos")
                args.Result = Mathf.Cos(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "tan")
                if (Mathf.Tan(float.Parse(args.Parameters[0].Evaluate().ToString())) > 100 ||
                    float.Parse(args.Parameters[0].Evaluate().ToString()) < -100)
                    args.Result = Mathf.Infinity;
                else
                    args.Result = Mathf.Tan(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "arcsin")
                args.Result = Mathf.Asin(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "arccos")
                args.Result = Mathf.Acos(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "arctan")
                args.Result = Mathf.Atan(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "sqrt")
                args.Result = Mathf.Sqrt(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "exp")
                args.Result = Mathf.Exp(float.Parse(args.Parameters[0].Evaluate().ToString()));
            else if (name == "log")
                if (Convert.ToDouble(args.Parameters[0].Evaluate().ToString()) > 0 &&
                    Convert.ToDouble(args.Parameters[1].Evaluate().ToString()) > 0 &&
                    Convert.ToDouble(args.Parameters[0].Evaluate().ToString()) != 0d)
                    args.Result = Mathf.Log(float.Parse(args.Parameters[0].Evaluate().ToString()),
                        float.Parse(args.Parameters[1].Evaluate().ToString()));
                else
                    args.Result = double.NaN;
            if (name == "abs") args.Result = Mathf.Abs(float.Parse(args.Parameters[0].Evaluate().ToString()));
            if (name == "pow")
                args.Result = Mathf.Pow(float.Parse(args.Parameters[0].Evaluate().ToString()),
                    float.Parse(args.Parameters[1].Evaluate().ToString()));
        };
        e.Parameters["pi"] = Mathf.PI;
        e.Parameters["e"] = 2.7183;
    }
}