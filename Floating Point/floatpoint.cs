/*Feroz Mughal
 *CSC 4101
 *IEEE 754 Floating Point Converter
 */ 

using System;

namespace ConsoleApp
{	
    public class Program
    {			
        public static void Main(string[] args)        {	
		int bias = 127; //preset bias for single precision
		int signBit = 0; //0 by default for positive values
		int remCounter = 0; // integer that declares total remainders
		int sigLength; //used with mantissa when forming Significand, both precisions
			
		string mantissa = ""; // Mantissa for Single Precision declared
		string remainder = ""; //used to store values during division loops
		
        Console.WriteLine("Enter a decimal floating-point number here: ");
        decimal num = decimal.Parse(Console.ReadLine());
		decimal temp = num; //stores num before absolute value is taken
		
		//if num is negative
        if (num < 0) {
            num = num *(-1); // remove -
            signBit = 1; // signBit change 0 -> 1
        }
        
        while (num >= 2){
            num = num / 2;
            remCounter++; // keeps track of division till result is less than 2
        }

        int expField = bias + remCounter; // Calculates the decimal value of exponent field and exponent
		int biasCalc = expField;
        
		int j;
        for (j = expField; j > 0; j = j/2){ //loop for calculating remainder
			remainder = (expField % 2) + remainder;
            expField = expField / 2;
		} 

        char[] remArray = remainder.ToCharArray();
        remainder = new string(remArray);

        Console.WriteLine("---------------------------\n");
        Console.WriteLine("Single precision (32 bits):\n"); //Label
        Console.WriteLine("Bit 31 Sign Bit: " + signBit); //Sign bit
        Console.WriteLine("Bits 30 - 23 Exponent Field: " + remainder); //binary exponent field

        //subtracting the number by itself via casting to separate the digits before and after the decimal point
        int beforeDecimal = (int)Math.Abs(temp);
        double afterDecimal = (double)(Math.Abs(temp) - beforeDecimal);
        string tempString = "";

        while (beforeDecimal > 0){ 
            tempString = tempString + beforeDecimal % 2; 
            beforeDecimal = beforeDecimal / 2;
        }

		if (temp != 0){
        	mantissa += tempString.Substring(1,tempString.Length-1);

        	while(afterDecimal > 0 && mantissa.Length - 1 <= 23){
            	afterDecimal = afterDecimal * 2;
            	int digits = (int)afterDecimal;
            	string sig = digits.ToString();
            	mantissa += sig;
            	afterDecimal = afterDecimal - digits;
        	}

        	if(mantissa.Length - 1 <= 23){
            	sigLength = 23 - mantissa.Length;
            	for(int i=0; i < sigLength; i++){
                	mantissa += "0";
            	}
        	}
				Console.WriteLine("Bits 22 - 0 Significand: 1." + mantissa);
		}
		
		//error handling as using 0 as an input currently causes error
		else if (temp ==0) {
			Console.WriteLine("Bits 22 - 0 Significand: 0.0000000000000000000000"); //sorry!
		}
		
		Console.WriteLine("\nDecimal value of exponent field and exponent:");
		Console.WriteLine("" + biasCalc + " - " + bias + " = " + remCounter + "\n"); //bias calculations

        //reset variables for Double Precision conversion
		bias = 1023;
		mantissa = "";
		remainder = "";
			
        expField = bias + remCounter; 
        biasCalc = expField;
		
        for (j = expField; j > 0; j = j/2){
			remainder = (expField % 2) + remainder;
            expField = expField / 2;
		} 

        remArray = remainder.ToCharArray();
        remainder = new string(remArray);
        
		Console.WriteLine("---------------------------\n");
        Console.WriteLine("Double precision (64 bits):\n");
			
        Console.WriteLine("Bit 63 Sign Bit: " + signBit);
        Console.WriteLine("Bits 62 - 52 Exponent Field: " + remainder);
			
		if (temp != 0){
        	mantissa += tempString.Substring(1, tempString.Length - 1);

        	while (afterDecimal > 0 && mantissa.Length - 1 <= 51){
            	afterDecimal = afterDecimal * 2;
            	int digits = (int)afterDecimal;
            	string sig = digits.ToString();
            	mantissa += sig;
            	afterDecimal = afterDecimal - digits;
        	}

        	if (mantissa.Length - 1 <= 51){
            	sigLength = 52 - mantissa.Length;
            	for (int i = 0; i < sigLength; i++){
                	mantissa += "0";
            	}
        	}
  
     		Console.WriteLine("Bits 51 - 0 Significand: 1." + mantissa);
		}
		else if (temp == 0){
			Console.WriteLine("Bits 51 - 0 Significand: 0.000000000000000000000000000000000000000000000000000"); //sorry! x2
		}
			Console.WriteLine("\nDecimal value of exponent field and exponent:");
			Console.WriteLine("" + biasCalc + " - " + bias + " = " + remCounter);
        }
    }
}