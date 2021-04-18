//  here I use a static method style rather than an object-oriented programming style for simplicity. The Main method has all the control logic. 
// The demo program isn’t quite as complicated as it might first appear because most of the nine methods are short helpers.

using System;
namespace ChiSquaredUsingCSharp
{
  class ChiSquaredProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Begin demo \n");
      // 1. Calculate chi-squared stat.
      // 2. Use chi-squared to calculate p-value.
      Console.WriteLine("End demo");
      Console.ReadLine();
    }
    public static void ShowVector(int[] v) { . . }
    public static void ShowVector(double[] v,
      int dec) { . . }
    public static double ChiFromFreqs(int[] observed,
      double[] expected) { . . }
    public static double ChiFromProbs(int[] observed,
      double[] probs) { . . }
    public static double[] ExpectedFromProbs(
      double[] probs, int N) { . . }
    public static double ChiSquarePval(double x,
      int df) { . . }
    private static double Exp(double x) { . . }
    public static double Gauss(double z) { . . }
  } // Program
} // ns
