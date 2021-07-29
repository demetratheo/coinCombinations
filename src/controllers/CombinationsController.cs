using System;
using System.Linq;
using System.Collections.Generic;

namespace CoinCombinations
{
    public class CombinationsController
    {
      public void printCombinations(int[] coins, int amount)
      {
        Console.WriteLine(
          "The available coins are '{0}'. The amount is {1}",
          string.Join(",", coins),
          amount
        );

        var combinations =this.createMatrix(coins.OrderByDescending(s => s).ToArray(), amount);
        var combinationStrings = combinations.Select((combs=> string.Join(" | ", combs.Select(c=> $"{c.coin}:{c.times}"))));
        foreach (var combination in combinationStrings.GroupBy((g)=> g).Select(g=> g.Key)){
          Console.WriteLine(combination);
        }
      }
      
      private List<List<Combination>> createMatrix(int[] coins, int amount){ 
        var mainList = new List<List<Combination>>();
        foreach (int coin in coins)
        {
          var cointimes = (double) amount / (double) coin;
          var roundedTimes = (int)Math.Floor(cointimes);
          while (roundedTimes > 0) {
            var remainingCoins = coins.Where(c=> c != coin);
            var items = new List<Combination>(){new Combination{coin = coin, times = roundedTimes}};
            var remainingAmount = amount - (roundedTimes * coin);
            foreach(int remainingCoin in remainingCoins){
              remainingCoins = remainingCoins.Where(c=> c != remainingCoin);
              var remainingCoinTime = (double) remainingAmount / (double) remainingCoin;
              if((cointimes % 1 == 0) && (remainingCoinTime % 1 == 0)){
                items.Add(new Combination{coin = remainingCoin, times = (int) remainingCoinTime});
                remainingAmount = remainingAmount - ((int)Math.Floor(remainingCoinTime) * remainingCoin);
              } else if(remainingCoins.Count() == 0) {
                items.Add(new Combination{coin = remainingCoin, times = 0});
              } else {
                items.Add(new Combination{coin = remainingCoin, times = (int)Math.Floor(remainingCoinTime)});
                remainingAmount = remainingAmount - ((int)Math.Floor(remainingCoinTime) * remainingCoin);
              }
            }
            items = items.OrderBy(c=> c.coin).ToList();
            if(items.Sum(combination=> combination.coin * combination.times) == amount){
              mainList.Add(items);
            }
            roundedTimes = roundedTimes -1;
          }
        }
        return mainList;
      }
    }
}
