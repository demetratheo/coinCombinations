using System.Linq;

namespace CoinCombinations
{
  class Program
  {
    static void Main(string[] args)
    {
        int[] coins = getCoins(args);
        int amount = getAmount(args);
        var combinationsController = new CombinationsController();
        combinationsController.printCombinations(coins, amount);
    }
    
		static int getAmount(string[] args){
      var defaultAmount = 10;
      if(args.Length < 2) return defaultAmount;
      int amount;
      int.TryParse(args[1], out amount);
      return amount;
    }
    
		static int[] getCoins(string[] args){
      var defaultCoins = new int[]{1, 2, 5};
      if(args.Length == 0) return defaultCoins;
      var parseCoins = args[0].Split(",");
      if(parseCoins.Length == 0) return defaultCoins;
      
      var givenCoins = new int[]{};
      foreach(var stringCoin in parseCoins) {
          int coin;
          if (int.TryParse(stringCoin, out coin)){
              givenCoins = givenCoins.Append(coin).ToArray();
          }
      }
      return givenCoins;
    }
  }
}
