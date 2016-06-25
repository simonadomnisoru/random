namespace RockPaperScissors.Controllers
{
    using RockPaperScissors.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    public class SimpleAIController : ApiController
    {      

        public OptionSelectedViewModel Get(int move)
        {
            int randomMove= -1;
            int roundCount=-1;
            List<HistoryMovesList> historyMoves= new List<HistoryMovesList>();

            OptionSelectedViewModel model = new OptionSelectedViewModel();
            if (move == (int)Moves.NotSelected)
            {
                model.Result = Moves.NotSelected.ToString();
                return model;
            }

            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                    if (ctx.Cache["historyMoves"] != null && ctx.Cache["roundCount"] != null)
                    {
                        roundCount = (int)ctx.Cache["roundCount"];
                        historyMoves = (List<HistoryMovesList>)ctx.Cache["historyMoves"];
                    }else
                    {
                        roundCount = 1;
                        historyMoves = new List<HistoryMovesList> {
                            new HistoryMovesList { Move = (int)Moves.Paper},
                            new HistoryMovesList { Move = (int)Moves.Rock},
                            new HistoryMovesList { Move = (int)Moves.Scissors}
                        };
                    }
            }

            if (roundCount < 3)
            {
                Random rnd = new Random();
                randomMove = rnd.Next((int)Moves.Rock, (int)Moves.Scissors);
            }
            else
            {
                var movesList = historyMoves.OrderByDescending(m => m.Score);
                randomMove = WiningMove(movesList.FirstOrDefault().Move);              
            }

            model.Id = randomMove;
            switch (randomMove)
                {
                    case 0:
                        {
                            switch (move)
                            {
                                case 0:
                                    {
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
                            break;
                        }
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
                                        break;
                                    }
                            }
                            break;
                        }
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
                            break;
                        }
                }

            roundCount += 1;
            if (ctx != null)
            {
               ctx.Cache["historyMoves"] = historyMoves;
               ctx.Cache["roundCount"] = roundCount;
            }
            return model;
        }

        public IHttpActionResult Restart() {
            try
            {
                var ctx = HttpContext.Current;
                if (ctx != null)
                {
                    if (ctx.Cache["historyMoves"] != null && ctx.Cache["roundCount"] != null)
                    {
                        ctx.Cache.Remove("roundCount");
                        ctx.Cache.Remove("historyMoves");
                    }
                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception e) {
                return BadRequest();
            }
        }

        /// <summary>
        ///  Returns the move that beats the input move
        /// </summary>
        /// <param move="move"></param>
        private int WiningMove(int move) {

            switch (move)
            {
                case 0:
                    {
                        return (int)Moves.Paper;
                    }
                case 1:
                    {
                        return (int)Moves.Scissors;
                    }
                case 2:
                    {
                        return (int)Moves.Rock;
                    }
                default:
                    {
                        return (int)Moves.NotSelected;
                    }
            }
        }        
    }
}
