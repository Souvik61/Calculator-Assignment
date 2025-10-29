using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{

    [Header("Buttons")]

    [SerializeField] private Button acButton;
    [SerializeField] private Button ceButton;
    [SerializeField] private Button divButton;
    [SerializeField] private Button mulButton;
    [SerializeField] private Button substractButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button equalButton;
    [SerializeField] private Button pointButton;
    [SerializeField] private Button zeroButton;
    [SerializeField] private Button oneButton;
    [SerializeField] private Button twoButton;
    [SerializeField] private Button threeButton;
    [SerializeField] private Button fourButton;
    [SerializeField] private Button fiveButton;
    [SerializeField] private Button sixButton;
    [SerializeField] private Button sevenButton;
    [SerializeField] private Button eightButton;
    [SerializeField] private Button nineButton;

    [Header("Others")]

    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text expressionText;

    public string currentExpression;
    public float currentResult;

    private void OnEnable()
    {
        // initializing buttons
        acButton.onClick.AddListener(OnAC);
        ceButton.onClick.AddListener(OnCE);
        divButton.onClick.AddListener(OnDivision);
        mulButton.onClick.AddListener(OnMultiply);
        substractButton.onClick.AddListener(OnSubstract);
        addButton.onClick.AddListener(OnAdd);
        equalButton.onClick.AddListener(OnEqual);
        pointButton.onClick.AddListener(OnPoint);
        zeroButton.onClick.AddListener(OnZero);
        oneButton.onClick.AddListener(OnOne);
        twoButton.onClick.AddListener(OnTwo);
        threeButton.onClick.AddListener(OnThree);
        fourButton.onClick.AddListener(OnFour);
        fiveButton.onClick.AddListener(OnFive);
        sixButton.onClick.AddListener(OnSix);
        sevenButton.onClick.AddListener(OnSeven);
        eightButton.onClick.AddListener(OnEight);
        nineButton.onClick.AddListener(OnNine);
    }

    private void OnDisable()
    {
        acButton.onClick.RemoveListener(OnAC);
        ceButton.onClick.RemoveListener(OnCE);
        divButton.onClick.RemoveListener(OnDivision);
        mulButton.onClick.RemoveListener(OnMultiply);
        substractButton.onClick.RemoveListener(OnSubstract);
        addButton.onClick.RemoveListener(OnAdd);
        equalButton.onClick.RemoveListener(OnEqual);
        pointButton.onClick.RemoveListener(OnPoint);
        zeroButton.onClick.RemoveListener(OnZero);
        oneButton.onClick.RemoveListener(OnOne);
        twoButton.onClick.RemoveListener(OnTwo);
        threeButton.onClick.RemoveListener(OnThree);
        fourButton.onClick.RemoveListener(OnFour);
        fiveButton.onClick.RemoveListener(OnFive);
        sixButton.onClick.RemoveListener(OnSix);
        sevenButton.onClick.RemoveListener(OnSeven);
        eightButton.onClick.RemoveListener(OnEight);
        nineButton.onClick.RemoveListener(OnNine);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentExpression = "";
        currentResult = 0;
        resultText.text = "0";
        expressionText.text = "";

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Function to return precedence of operators
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    int Precedence(string c)
    {
        if (c == "/" || c == "*")
            return 1;
        else if (c == "+" || c == "-")
            return 0;
        else
            return -1;
    }

    /// <summary>
    /// Checks if this string is a operator
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    bool IsOperator(string s)
    {
        return s == "*" || s == "/" || s == "+" || s == "-";
    }

    /// <summary>
    /// Convert a infix expression to postfix using stack
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    List<string> InfixToPostfix(List<string> expr)
    {
        Stack<string> st = new Stack<string>();
        List<string> res = new List<string>();

        for (int i = 0; i < expr.Count; i++)
        {
            string token = expr[i];

            // If operand, add to result
            if (!IsOperator(token))
            {
                res.Add(token);
            }// If operator
            else
            {
                while (st.Count > 0 && Precedence(st.Peek()) >= Precedence(token))
                {
                    res.Add(st.Pop());
                }
                st.Push(token);
            }
        }

        // Pop remaining operators
        while (st.Count > 0)
        {
            res.Add(st.Pop());
        }

        return res;
    }

    /// <summary>
    /// Evaluate a postfix expression
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public float EvaluatePostfix(List<string> arr)
    {
        Stack<float> st = new Stack<float>();

        foreach (string token in arr)
        {

            // If it's an operand (number), push it onto the stack
            if (!IsOperator(token))
            {
                st.Push(float.Parse(token));
            }

            // Otherwise, it must be an operator
            else
            {
                float val1 = st.Pop();
                float val2 = st.Pop();

                if (token == "+") st.Push(val2 + val1);
                else if (token == "-") st.Push(val2 - val1);
                else if (token == "*") st.Push(val2 * val1);
                else if (token == "/") st.Push(val2 / val1);
            }
        }
        return st.Pop();
    }


    /// <summary>
    /// Given the expression in infix get output result
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    float EvaluateExpression(List<string> inp)
    {
        float outp = 0.0f;
        var post = InfixToPostfix(inp);
        outp = EvaluatePostfix(post);

        return outp;
    }

    /// <summary>
    /// Takes a string and converts to a list of strings separated by operators
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    List<string> StringToTokens(string str)
    {
        List<string> outp = new List<string>();
        string curr = "";

        //Iterate through the input and check if it is operator or operand
        for (int i = 0; i < str.Length; i++)
        {
            string c = str[i].ToString();

            if (!IsOperator(c))
            {
                curr += c;
            }
            else
            {
                outp.Add(curr);
                outp.Add(c);
                curr = "";
            }
        }

        if (!string.IsNullOrEmpty(curr))
            outp.Add(curr);

        return outp;
    }

    void UpdateDisplay()
    {
        resultText.text = currentResult.ToString("0.##");
        expressionText.text = currentExpression;
    }

    /// <summary>
    /// Get last character in expression input
    /// </summary>
    /// <returns></returns>
    string GetLastCharacter()
    {
        if (currentExpression.Length > 0)
        {
            return currentExpression[currentExpression.Length - 1].ToString();
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// Check if the value is almost zero
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    bool AlmostZero(float val)
    {
        return val > -0.000001f && val < 0.000001f;
    }

    /// <summary>
    /// Is the given expression valid
    /// </summary>
    /// <returns></returns>
    bool IsExpressionValid(string expr)
    {
        if (string.IsNullOrEmpty(expr)) return true;

        string c = expr[expr.Length - 1].ToString();

        return !IsOperator(c);
    }

    //--------------------
    // Button callbacks
    //--------------------

    void OnAC()
    {
        currentExpression = "";
        currentResult = 0;
        UpdateDisplay();

        Debug.Log("OnAC");
    }

    void OnCE()
    {
        currentExpression = "";
        UpdateDisplay();
        Debug.Log("OnCE");
    }

    void OnDivision()
    {
        if (currentExpression == "" && !AlmostZero(currentResult))
        {
            currentExpression = currentResult.ToString("0.##");
        }

        if (currentExpression == "") return;
        //if is last character operator if yes the replace otherwise append
        if (IsOperator(currentExpression[currentExpression.Length - 1].ToString()))
        {
            currentExpression = currentExpression.Substring(0, currentExpression.Length - 1) + "/";
        }
        else
        {
            currentExpression += "/";
        }

        UpdateDisplay();
    }

    void OnMultiply()
    {
        if (currentExpression == "" && !AlmostZero(currentResult))
        {
            currentExpression = currentResult.ToString("0.##");
        }

        if (currentExpression == "") return;

        //if is last character operator if yes the replace otherwise append
        if (IsOperator(currentExpression[currentExpression.Length - 1].ToString()))
        {
            currentExpression = currentExpression.Substring(0, currentExpression.Length - 1) + "*";
        }
        else
        {
            currentExpression += "*";
        }
        UpdateDisplay();

    }

    void OnSubstract()
    {
        if (currentExpression == "" && !AlmostZero(currentResult))
        {
            currentExpression = currentResult.ToString("0.##");
        }

        if (currentExpression == "") return;

        //if is last character operator if yes the replace otherwise append
        if (IsOperator(currentExpression[currentExpression.Length - 1].ToString()))
        {
            currentExpression = currentExpression.Substring(0, currentExpression.Length - 1) + "-";
        }
        else
        {
            currentExpression += "-";
        }
        UpdateDisplay();

    }

    void OnAdd()
    {
        if (currentExpression == "" && !AlmostZero(currentResult))
        {
            currentExpression = currentResult.ToString("0.##");
        }

        if (currentExpression == "") return;

        //if is last character operator if yes the replace otherwise append
        if (IsOperator(currentExpression[currentExpression.Length - 1].ToString()))
        {
            currentExpression = currentExpression.Substring(0, currentExpression.Length - 1) + "+";
        }
        else
        {
            currentExpression += "+";
        }
        UpdateDisplay();
    }

    void OnEqual()
    {
        if (currentExpression == "") return;

        bool flag = IsExpressionValid(currentExpression);
        if (flag)
        {
            var tokens = StringToTokens(currentExpression);
            float res = EvaluateExpression(tokens);
            currentResult = res;
            resultText.text = currentResult.ToString("0.##");
        }
        else
        {
            currentResult = 0;
            currentExpression = "";
            UpdateDisplay();
        }
    }

    void OnPoint()
    {
        if (currentExpression != "" && !IsOperator(currentExpression[currentExpression.Length - 1].ToString()) && GetLastCharacter() != ".")
        {
            currentExpression += ".";
        }

        UpdateDisplay();
    }

    void OnZero()
    {
        if (currentExpression != "0")
        {
            currentExpression += "0";
        }

        UpdateDisplay();
    }

    void OnOne()
    {
        currentExpression += "1";
        UpdateDisplay();

    }

    void OnTwo()
    {
        currentExpression += "2";
        UpdateDisplay();

    }

    void OnThree()
    {
        currentExpression += "3";
        UpdateDisplay();

    }

    void OnFour()
    {
        currentExpression += "4";
        UpdateDisplay();

    }

    void OnFive()
    {
        currentExpression += "5";
        UpdateDisplay();

    }

    void OnSix()
    {
        currentExpression += "6";
        UpdateDisplay();

    }

    void OnSeven()
    {
        currentExpression += "7";
        UpdateDisplay();

    }

    void OnEight()
    {
        currentExpression += "8";
        UpdateDisplay();

    }

    void OnNine()
    {
        currentExpression += "9";
        UpdateDisplay();

    }
}
