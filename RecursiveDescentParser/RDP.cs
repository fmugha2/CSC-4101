/*Feroz Mughal
  CSC 4101
  Recursive Descent Parser
*/

//test with "num1 + num2 / (3 + total)" input expression

using System;
using System.Collections.Generic;

namespace Recursive_Descent_Parser
{

    class Lexer
    {
        private static List<string> Tokens;
        public bool Success = false;
        public List<string> tokens { get { if (Success) return Tokens; else return null; } }

        ///This Tokenizes the user input
        public void Tokenize(string input)
        {
            Tokens = new List<string>();
            Success = true;
            string lexeme = "";
            int index = 0;

            while (index < input.Length)
            {
                if (Char.IsLetter(input[index]))
                {
                    while (Char.IsLetterOrDigit(input[index]))
                    {
                        lexeme += input[index];
                        if (index <= input.Length - 1)
                            index++;
                        else
                            break;
                    }
                    Tokens.Add("IDENT");
                    Console.WriteLine(lexeme + ":IDENT");
                    lexeme = "";
                }

                else if (Char.IsDigit(input[index]))
                {
                    while (Char.IsLetterOrDigit(input[index]))
                    {
                        lexeme += input[index];
                        if (index <= input.Length - 1)
                            index++;
                        else
                            break;
                    }
                    Tokens.Add("INT_LIT");
                    Console.WriteLine(lexeme + ":INT_LIT");
                    lexeme = "";
                }

                else if (isOperator(input[index]))
                {
                    Tokens.Add(LookUp(input[index]));
                    Console.WriteLine(input[index] + ":" + LookUp(input[index]));
                    if (!(index == input.Length - 1))
                        index++;
                    else
                        break;
                }

                else if (String.IsNullOrWhiteSpace(input[index].ToString()))
                    index++;

                else
                {
                    Console.WriteLine("Error: Lexer Detected Invalid Input (" + index + ")");
                    Success = false;
                    break;
                }
            }
            Tokens.Add("EOF");
        }

        private bool isOperator(char lexeme)
        {
            if (lexeme == '+' ||
                lexeme == '-' ||
                lexeme == '/' ||
                lexeme == '*' ||
                lexeme == '(' ||
                lexeme == ')')
                return true;

            return false;
        }

        private string LookUp(char lexeme)
        {
            Dictionary<char, string> Tokens = new Dictionary<char, string>();
            Tokens.Add('=', "ASSIGN");
            Tokens.Add('+', "ADD_OP");
            Tokens.Add('-', "SUB_OP");
            Tokens.Add('*', "MUL_OP");
            Tokens.Add('/', "DIV_OP");
            Tokens.Add('(', "LEFT_PAREN");
            Tokens.Add(')', "RIGHT_PAREN");

            if (Tokens.ContainsKey(lexeme))
                return Tokens[lexeme];
            else
                return "Error: Lexer Detected Invalid Input (" + lexeme + ")";
        }
    }
    ///Parsers the tokens to build Parse Tree
    class RecursiveDescentParser
    {
        private string nextToken;
        private int tokenIndex = 0;
        private List<String> Tokens;

        public void Start(List<string> tokens)
        {
            Tokens = new List<string>(tokens);
            lex();
            expr();
        }

        private void expr()
        {
            Console.WriteLine("Enter <expr>");
            term();
            while (nextToken == "ADD_OP" || nextToken == "SUB_OP")
            {
                lex();
                term();
            }
            Console.WriteLine("Exit <expr>");
        }

        private void term()
        {
            Console.WriteLine("Enter <term>");
            factor();
            while (nextToken == "MUL_OP" || nextToken == "DIV_OP")
            {
                lex();
                factor();
            }
            Console.WriteLine("Exit <term>");
        }

        private void factor()
        {
            Console.WriteLine("Enter <factor>");
            if (nextToken == "IDENT" || nextToken == "INT_LIT")
            {
                lex();
                //Console.WriteLine("lex");
            }
            else
            {
                if (nextToken == "LEFT_PAREN")
                {
                    //Console.WriteLine("left_paren test");
                    lex();
                    expr();

                    if (nextToken == "RIGHT_PAREN")
                    {

                        //Console.WriteLine("right_paren test");
                        lex();
                    }

                    else
                    {
                        //Console.WriteLine("first error test");
                        error(nextToken);
                    }
                }
                else
                    //Console.WriteLine("second error test");
                    error(nextToken);
            }
            Console.WriteLine("Exit <factor>");
        }

        private void lex()
        {
            nextToken = Tokens[tokenIndex];
            Console.WriteLine("NextToken: " + Tokens[tokenIndex]);
            tokenIndex++;
        }

        private void error(string token)
        {
            Console.WriteLine("Error"); //add detail to this
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter your expression: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Calling Lexer:");
            Lexer lexer = new Lexer();
            lexer.Tokenize(userInput);

            if (lexer.tokens != null)
            {
                RecursiveDescentParser RDP = new RecursiveDescentParser();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Calling Recursive Descent Parser:");
                RDP.Start(lexer.tokens);
            }
        }
    }
}