namespace SquadCapgeminiTest.Helpers
{
    public class TokenGenerator
    {
        public long GenerateToken(long cardNumber, int cvv)
        {
            string lastFourDigits;
            string cardNumberString = cardNumber.ToString();
            if (cardNumber > 9999)
            {
                lastFourDigits = cardNumberString.Substring(cardNumberString.Length - 4);
            }
            else
            {
                lastFourDigits = cardNumber.ToString();
            }
             

            int[] lastFourDigitsArray = lastFourDigits.Select(t => int.Parse(t.ToString())).ToArray();

            

            int temp;
            for (int i = 0; i < cvv; i++)
            {
                for (int j = 0; j < lastFourDigitsArray.Length - 1; j++)
                {
                    temp = lastFourDigitsArray[0];
                    lastFourDigitsArray[0] = lastFourDigitsArray[j + 1];
                    lastFourDigitsArray[j + 1] = temp;
                }
            }

            long number = long.Parse(string.Concat(lastFourDigitsArray));

            return number;

        }
    }
}
