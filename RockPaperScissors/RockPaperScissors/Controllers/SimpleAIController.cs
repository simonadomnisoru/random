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
        private List<HistoryMovesList> historyMoves;
        private int roundCount;

        public SimpleAIController() {
            historyMoves = new List<HistoryMovesList> {
                new HistoryMovesList { Move = (int)Moves.Paper},
                new HistoryMovesList { Move = (int)Moves.Rock},
                new HistoryMovesList { Move = (int)Moves.Scissors}
            };
            roundCount = 0;
        }

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
                                    historyMoves.Where(m => m.Move == (int)Moves.Rock).FirstOrDefault().Score += 1;
                                    break;
                                }
                            case 1:
                                {
                                    model.Result = Result.Win.ToString();
                                    model.TypeResult = (int)Result.Win;
                                    historyMoves.Where(m => m.Move == (int)Moves.Paper).FirstOrDefault().Score += 1;
                                    break;
                                }
                            case 2:
                                {
                                    model.Result = Result.Lose.ToString();
                                    model.TypeResult = (int)Result.Lose;
                                    historyMoves.Where(m => m.Move == (int)Moves.Scissors).FirstOrDefault().Score += 1;
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
                                    historyMoves.Where(m => m.Move == (int)Moves.Rock).FirstOrDefault().Score += 1;
                                    break;
                                }
                            case 1:
                                {
                                    model.Result = Result.Tie.ToString();
                                    model.TypeResult = (int)Result.Tie;
                                    historyMoves.Where(m => m.Move == (int)Moves.Paper).FirstOrDefault().Score += 1;
                                    break;
                                }
                            case 2:
                                {
                                    model.Result = Result.Win.ToString();
                                    model.TypeResult = (int)Result.Win;
                                    historyMoves.Where(m => m.Move == (int)Moves.Scissors).FirstOrDefault().Score += 1;
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
                                    model.TypeResult = (int)Result.Win;
                                    historyMoves.Where(m => m.Move == (int)Moves.Rock).FirstOrDefault().Score += 1;
                                    break;
                                }
                            case 1:
                                {
                                    model.Result = Result.Lose.ToString();
                                    model.TypeResult = (int)Result.Lose;
                                    historyMoves.Where(m => m.Move == (int)Moves.Paper).FirstOrDefault().Score += 1;
                                    break;
                                }
                            case 2:
                                {
                                    model.Result = Result.Tie.ToString();
                                    model.TypeResult = (int)Result.Tie;
                                    historyMoves.Where(m => m.Move == (int)Moves.Scissors).FirstOrDefault().Score += 1;
                                    break;
                                }
                        }
                        break; }
            }
            roundCount += 1;
            return model;
        }

        public IHttpActionResult Restart() {
            try
            {
                roundCount = 0;
                historyMoves.ForEach(m => m.Score = 0);
                return Ok();
            }
            catch(Exception e) {
                return BadRequest();
            }
        }

        //[TODO] - if still time - add some learning AI 
    }
}
