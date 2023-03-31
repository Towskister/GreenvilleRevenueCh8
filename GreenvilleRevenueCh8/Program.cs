using System;
using static System.Console;
using System.Globalization;
class GreenvilleRevenue
{
    public static string nl = Environment.NewLine;

    static void Main()
    {
        const int MIN_CONTESTANTS = 0;
        const int MAX_CONTESTANTS = 30;
        const double ENTRANCE_FEE = 25;
        string nl = Environment.NewLine;

        int numContestantsLastYear, numContestantsThisYear;
        string[] contestantNames = new string[MAX_CONTESTANTS];
        char[] contestantTalents = new char[MAX_CONTESTANTS];
        char[] talentCodes = { 'S', 'D', 'M', 'O' };
        string[] talentStrings = { "Singing", "Dancing", "Musical instrument", "Other" };
        int[] talentCounts = { 0, 0, 0, 0 };
        double revenue;

        numContestantsLastYear = getContestantNumber("last", MIN_CONTESTANTS, MAX_CONTESTANTS);
        numContestantsThisYear = getContestantNumber("this", MIN_CONTESTANTS, MAX_CONTESTANTS);

        WriteLine($"Last year's competition had {numContestantsLastYear} contestants, and this year's has {numContestantsThisYear} contestants");

        revenue = numContestantsThisYear * ENTRANCE_FEE;
        WriteLine("Revenue expected this year is {0}", revenue.ToString("C", CultureInfo.GetCultureInfo("en-US")));

        displayRelationship(numContestantsThisYear, numContestantsLastYear);

        getContestantData(numContestantsThisYear, contestantNames, contestantTalents, talentCodes, talentStrings, talentCounts);

        getLists(numContestantsThisYear, talentCodes, talentStrings, contestantNames, contestantTalents, talentCounts);

    }

    public static int getContestantNumber(string competitionYear, int min, int max)
    {
        int numContestants = -1;
        while (numContestants < min || numContestants > max)
        {
            Write($"Enter number of contestants {competitionYear} year >> ");
            if (int.TryParse(ReadLine(), out numContestants))
            {
                if (numContestants < min || numContestants > max)
                {
                    WriteLine($"invalid input. Number of contestants must be between {min} and {max}. Please try again.");
                }
            }
            else
            {
                WriteLine("invalid input. must enter an integer");
                numContestants = -1; // This is necessisary to reinitialize the numContestants back to -1 as TryParse will deault the int variable to 0 
            }
        }
        return numContestants;
    }

    public static void displayRelationship(int numContestantsThisYear, int numContestantsLastYear)
    {
        if (numContestantsThisYear > numContestantsLastYear)
        {
            WriteLine("The competition is bigger than ever!");
        }
        else if (numContestantsThisYear < numContestantsLastYear)
        {
            WriteLine("A tighter race this year! Come out and cast your vote!");
        }
        else
        {
            WriteLine("The competition is the same as last year - BORING!");
        }
    }
    public static void getContestantData(int numThisYear, string[] names, char[] talents, char[] talentCodes, string[] talentCodesStrings, int[] counts)
    {
        for (int i = 0; i < numThisYear; i++)
        {
            Write("Enter contestant's name: ");
            string nameInput = ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                WriteLine("invalid input. Please try again.");
                i--; // Decrement i so that the loop will ask for the same index again
                continue;
            }
            else if (nameInput.Length > 15)
            {
                WriteLine("invalid input. Name cannot be more than 15 characters.");
                i--; // Decrement i so that the loop will ask for the same index again
                continue;
            }
            names[i] = nameInput;

            bool validTalentCode = false;
            do
            {
                WriteLine("Talent codes are:");
                for (int a = 0; a < talentCodes.Length; a++)
                {
                    WriteLine($"  {talentCodes[a]}   {talentCodesStrings[a]}");
                }

                Write("Enter talent code >> ");

                if (char.TryParse(ReadLine(), out char tCodeInput))
                {
                    if (Array.IndexOf(talentCodes, tCodeInput) >= 0)
                    {
                        talents[i] = tCodeInput;
                        validTalentCode = true;
                    }
                    else
                    {
                        WriteLine("invalid talent code. Please try again.");
                    }
                }
                else
                {
                    WriteLine("invalid talent code. Please try again.");
                }
            } while (!validTalentCode);

            int talentIndex = Array.IndexOf(talentCodes, talents[i]);
            counts[talentIndex]++;
        }
    }
    public static void getLists(int numContestants, char[] talentCodes, string[] talentStrings, string[] names, char[] talents, int[] counts)
    {
        WriteLine("The types of talent are:");

        for (int i = 0; i < talentCodes.Length; ++i)
        {
            WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
        }
        WriteLine("Enter a talent code to see a list of contestants, or enter Z to quit");

        while (true)
        {
            string input = ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                WriteLine($"Invalid input.{nl}" +
                    "The types of talent are:");
                for (int i = 0; i < talentCodes.Length; ++i)
                {
                    WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                }
                WriteLine("Please enter the letter of a valid talent code or Z to quit.");
                continue;
            }

            if (!char.TryParse(input, out char tCodeInput))
            {
                WriteLine($"Invalid input.{nl}" +
                    "The types of talent are:");
                for (int i = 0; i < talentCodes.Length; ++i)
                {
                    WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                }
                WriteLine("Please enter only one character.");
                continue;
            }

            if (Array.IndexOf(talentCodes, tCodeInput) >= 0 || tCodeInput == 'Z')

                if (tCodeInput == 'Z')
            {
                break;
            }

            if (Array.IndexOf(talentCodes, tCodeInput) >= 0)
            {
                bool found = false;
                for (int i = 0; i < talentCodes.Length; i++)
                {
                    if (tCodeInput == talentCodes[i])
                    {
                        WriteLine($"Contestants with talent {talentStrings[i]} are:");
                        for (int j = 0; j < numContestants; j++)
                        {
                            if (talents[j] == talentCodes[i])
                            {
                                WriteLine($"{names[j]}");
                                found = true;
                            }
                        }
                    }
                }
                if (!found)
                {
                    WriteLine($"Invalid input.{nl}" +
                        "The types of talent are:");
                    for (int i = 0; i < talentCodes.Length; ++i)
                    {
                        WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                    }
                    WriteLine("Invalid talent code.");
                }
            }
            else
            {
                WriteLine($"Invalid input.{nl}" +
                    "The types of talent are:");
                for (int i = 0; i < talentCodes.Length; ++i)
                {
                    WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                }
                WriteLine("Please enter the letter of a valid talent code or Z to quit.");
            }

            WriteLine("The types of talent are:");
            for (int i = 0; i < talentCodes.Length; ++i)
            {
                WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
            }
            WriteLine("Enter a talent code to see a list of contestants, or enter Z to quit");
        }
    }

}