namespace RockPaperScissors.Controllers
{
    using RockPaperScissors.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class SimpleAIController : ApiController
    {
        public OptionSelectedViewModel Get(int move)
        {
            Random rnd = new Random();
            int randomMove = rnd.Next((int)Moves.Rock, (int)Moves.Scissors);

            OptionSelectedViewModel model = new OptionSelectedViewModel();
            model.Id = randomMove;
            if (move == (int)Moves.NotSelected) {
                model.Result = "Not selected";
                return model;
            }

            switch (randomMove) {
                case 0:
                    {
                        switch (move)
                        {
                            case 0: {
                                    model.Result = Result.Tie.ToString();
                                    model.TypeResult = (int)Result.Tie;
                                    break;
                                }
                            case 1:
                                {
                                    model.Result = Result.Win.ToString();
                                    model.TypeResult = (int)Result.Win;
                                    break;
                                }
                            case 2:
                                {
                                    model.Result = Result.Lose.ToString();
                                    model.TypeResult = (int)Result.Lose;
                                    break;
                                }
                        }
                        break; }
                case 1:
                    {
                        switch (move)
                        {
                            case 0:
                                {
                                    model.Result = Result.Lose.ToString();
                                    model.TypeResult = (int)Result.Lose;
                                    break;
                                }
                            case 1:
                                {
                                    model.Result = Result.Tie.ToString();
                                    model.TypeResult = (int)Result.Tie;
                                    break;
                                }
                            case 2:
                                {
                                    model.Result = Result.Win.ToString();
                                    model.TypeResult = (int)Result.Win;
                                    break;  }
                        }
                        break; }
                case 2:
                    {
                        switch (move)
                        {
                            case 0:
                                {
                                    model.Result = Result.Win.ToString();
                                    model.TypeResult = (int)Result.Lose;
                                    break;
                                }
                            case 1:
                                {
                                    model.Result = Result.Lose.ToString();
                                    model.TypeResult = (int)Result.Lose;
                                    break;
                                }
                            case 2:
                                {
                                    model.Result = Result.Tie.ToString();
                                    model.TypeResult = (int)Result.Tie;
                                    break;
                                }
                        }
                        break; }
            }

            return model;
        }

        //[TODO] - store results
        //[TODO] - retart game
        //[TODO] - if still time - add some learning AI 
    }
}
