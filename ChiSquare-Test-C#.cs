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



// To create the demo program, I launched Visual Studio and created a new C# console application program and named it ChiSquared­UsingCSharp. I used Visual Studio 2015, 
// but the demo program has no significant .NET dependencies so any recent version of Visual Studio will work.

// After the template code loaded into the editor window, I right-clicked on file Program.cs in the Solution Explorer window and renamed the file to ChiSquaredProgram.cs, 
// then allowed Visual Studio to automatically rename class Program for me. At the top of the template-generated code, I deleted all unnecessary using statements, leaving 
// just the one that references the top-level System namespace.

// The Main method sets up the observed counts of the roulette wheel like so:
Console.WriteLine("Is roulette wheel fair");
Console.WriteLine("Wheel was spun 380 times");
int[] observed = new int[] { 192, 163, 25 };  // 380
Console.WriteLine("Observed counts red, black, green:");
ShowVector(observed);


// I just made these observed counts up because they give a representative example. Helper method ShowVector that displays an integer array is defined:
public static void ShowVector(int[] v)  {
  for (int I = 0; i < v.Length; ++i)
    Console.Write(v[i] +“" “");
  Console.WriteLine“"\”");
}


// Next, instead of setting up the expected counts directly, I set them up indirectly like this:
double[] probs = new double[] {
  18.0/38, 18.0/38, 2.0/38 };
Console.WriteLine("Probabilities if fair:");
ShowVector(probs, 4);
double[] expected = ExpectedFromProbs(probs, 380);
Console.WriteLine("Expected counts if fair:");
ShowVector(expected, 1);


// The probabilities of red, black, and green are 18/38, 18/38, and 2/38, as explained earlier. 
// Helper method ExpectedFromProbs accepts an array of probabilities and a total count, and returns an array of expected counts. 
// I could have directly set up the expected counts for 380 spins of the roulette wheel like this:
double[] expected = new double[] { 180.0, 180.0, 20.0 };


// The expected-count helper method is defined:
public static double[] ExpectedFromProbs(double[] probs,
  int N)
{
  double[] expected = new double[probs.Length];
  for (int i = 0; i < probs.Length; ++i)
    expected[i] = probs[i] * N;
  return expected;
}


// And the overloaded ShowVector method for an array of type double is defined:
public static void ShowVector(double[] v, int dec) {
  for (int i = 0; i < v.Length; ++i)
    Console.Write(v[i].ToString("F" + dec) + "  ");
  Console.WriteLine("\n");
}


// The demo program continues by calculating the chi-squared statistic:
double chi = ChiFromProbs(observed, probs); 
Console.WriteLine("Calculated chi-squared = " +
  chi.ToString("F2"));  // 3.66


// Method ChiFromProbs uses a signature that accepts an integer array of observed counts and a double array of expected probabilities, 
// mostly because that’s the signature used by the equivalent R language chisq.test function. 
// For example, in an interactive R shell you could perform the demo like so:
> obs <- c(192, 163, 25)
> probs <- c(18/38, 18/38, 2/38)
> chisq.test(x=obs, p=probs)
X-squared = 3.6556, df = 2, p-value = 0.1608
  

 // Notice the calculated chi-squared statistic (3.66) and p-value (0.1608) obtained using R are the same values as computed by the demo program. 
 // The demo concludes by using the calculated chi-squared statistic to compute the p-value:
  int df = observed.Length - 1;
double pval = ChiSquarePval(chi, 2);
Console.WriteLine("The pval with df of " + df +
  " = " + pval.ToString("F4") );
Console.WriteLine("Pval is probability, if wheel fair,");
Console.WriteLine("you'd see a chi-squared as calculated");
Console.WriteLine("End demo");
Console.ReadLine();


// Variable df stands for degrees of freedom, which I’ll explain shortly.
// Understanding the Chi-Squared Statistic
// If you have an array of observed counts and an array of expected counts, you can calculate a metric called the chi-squared statistic, a measure of how different the observed and expected counts are. 
// Larger values indicate greater difference.
// The chi-squared statistic is defined as the sum of the squared differences between observed and expected divided by expected:
chi-squared = sum( (obs[i] - exp[i])^2 / exp[i] )
  

// The idea is best explained by an example. Suppose, as in the demo, the observed counts for 380 spins of a roulette wheel are (192, 163, 25) and the expected counts if the wheel is fair are (180, 180, 20). 
// The calculated chi-squared statistic is:
chi-squared = (192 - 180)^2 / 180 +
              (163 - 180)^2 / 180 +
              (25 - 20)^2   / 20
            = (144 / 180) + (289 / 180) + (25 / 20)
            = 0.8000 + 1.6056 + 1.2500
            = 3.6556
  
 
 // The demo implements this function as:
 public static double ChiFromFreqs(int[] observed,
  double[] expected)
{
  double sum = 0.0;
  for (int i = 0; i < observed.Length; ++i) {
    sum += ((observed[i] - expected[i]) *
      (observed[i] - expected[i])) / expected[i];
  }
  return sum;
}


// There’s no error-checking here, for simplicity, but in a production system you’d want to make sure that arrays observed and expected have the same length, and so on. 
// The demo program also has a method to calculate a chi-squared statistic from an array of observed counts and an array of expected probabilities:
public static double ChiFromProbs(int[] observed,
  double[] probs)
{
  int n = observed.Length;
  int sumObs = 0;
  for (int i = 0; i < n; ++i)
    sumObs += observed[i];
  double[] expected = ExpectedFromProbs(probs, sumObs);
  return ChiFromFreqs(observed, expected);
}


// To recap, the math definition of the chi-squared statistic uses an array of observed counts and an array of expected counts. 
// It’s also useful to calculate chi-squared from an array of observed counts and an array of expected probabilities. 
// To perform this calculation, it’s handy to have a helper method that calculates expected counts from expected probabilities.

//Understanding the Chi-Squared Distribution
// If you have a calculated chi-squared statistic value, you can use it to calculate the probability (p-value) of getting that chi-squared value. This idea is best explained visually. 
// Take a look at the graph in Figure 3, which shows the chi-squared distribution for the demo problem. >>> i.e. https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/march/test-run-chi-squared-goodness-of-fit-using-csharp
// The total area under any chi-squared distribution is 1.0. 
// The p-value is the area under the graph from the calculated chi-squared statistic to +infinity.
// There are several sophisticated algorithms that can calculate a p-value / area under the chi-squared distribution graph. The demo program uses what’s called ACM Algorithm 299, which is implemented as method ChiSquarePval. The algorithm in turn uses another algorithm called ACM 209, which is implemented as method Gauss. These algorithms are foundations of numerical computing and are presented in Figure 4. Even a brief glance at the code should convince you there’s some very serious math going on, but luckily you can think of the methods that implement these algorithms as black boxes because you’ll never have to modify the code.

// Figure 4 Methods ChiSquarePval and Gauss

public static double ChiSquarePval(double x, int df)
{
  // x = a computed chi-square value.
  // df = degrees of freedom.
  // output = prob. x value occurred by chance.
  // ACM 299.
  if (x <= 0.0 || df < 1)
    throw new Exception("Bad arg in ChiSquarePval()");
  double a = 0.0; // 299 variable names
  double y = 0.0;
  double s = 0.0;
  double z = 0.0;
  double ee = 0.0; // change from e
  double c;
  bool even; // Is df even?
  a = 0.5 * x;
  if (df % 2 == 0) even = true; else even = false;
  if (df > 1) y = Exp(-a); // ACM update remark (4)
  if (even == true) s = y;
  else s = 2.0 * Gauss(-Math.Sqrt(x));
  if (df > 2)
  {
    x = 0.5 * (df - 1.0);
    if (even == true) z = 1.0; else z = 0.5;
    if (a > 40.0) // ACM remark (5)
    {
      if (even == true) ee = 0.0;
      else ee = 0.5723649429247000870717135;
      c = Math.Log(a); // log base e
      while (z <= x) {
        ee = Math.Log(z) + ee;
        s = s + Exp(c * z - a - ee); // ACM update remark (6)
        z = z + 1.0;
      }
      return s;
    } // a > 40.0
    else
    {
      if (even == true) ee = 1.0;
      else
        ee = 0.5641895835477562869480795 / Math.Sqrt(a);
      c = 0.0;
      while (z <= x) {
        ee = ee * (a / z); // ACM update remark (7)
        c = c + ee;
        z = z + 1.0;
      }
      return c * y + s;
    }
  } // df > 2
  else {
    return s;
  }
} // ChiSquarePval()
private static double Exp(double x)
{
  if (x < -40.0) // ACM update remark (8)
    return 0.0;
  else
    return Math.Exp(x);
}
public static double Gauss(double z)
{
  // input = z-value (-inf to +inf)
  // output = p under Normal curve from -inf to z
  // ACM Algorithm #209
  double y; // 209 scratch variable
  double p; // result. called ‘z’ in 209
  double w; // 209 scratch variable
  if (z == 0.0)
    p = 0.0;
  else
  {
    y = Math.Abs(z) / 2;
    if (y >= 3.0)
    {
      p = 1.0;
    }
    else if (y < 1.0)
    {
      w = y * y;
      p = ((((((((0.000124818987 * w
        - 0.001075204047) * w + 0.005198775019) * w
        - 0.019198292004) * w + 0.059054035642) * w
        - 0.151968751364) * w + 0.319152932694) * w
        - 0.531923007300) * w + 0.797884560593) * y
        * 2.0;
    }
    else
    {
      y = y - 2.0;
      p = (((((((((((((-0.000045255659 * y
        + 0.000152529290) * y - 0.000019538132) * y
        - 0.000676904986) * y + 0.001390604284) * y
        - 0.000794620820) * y - 0.002034254874) * y
       + 0.006549791214) * y - 0.010557625006) * y
       + 0.011630447319) * y - 0.009279453341) * y
       + 0.005353579108) * y - 0.002141268741) * y
       + 0.000535310849) * y + 0.999936657524;
    }
  }
  if (z > 0.0)
    return (p + 1.0) / 2;
  else
    return (1.0 - p) / 2;
} // Gauss()


// Wrapping Up:
// It’s important to remember that the chi-squared goodness of fit test, like most statistical tests, is probabilistic, so you should interpret results conservatively. 
// Even if you get a very small p-value, it’s preferable to say 
// something like, “The small p-value of 0.0085 suggests that the difference between observed and expected counts is unlikely to have occurred by chance,” 
// rather than, “The small p-value indicates the observed counts couldn’t have occurred by chance.”
