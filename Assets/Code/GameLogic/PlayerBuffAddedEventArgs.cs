using Code.Data.GameDataTypes;

namespace Code.GameLogic
{
     public struct PlayerBuffAddedEventArgs
     {
         public Buff Buff { get; }
 
         public int Id { get; }
 
         public PlayerBuffAddedEventArgs(Buff buff, int id)
         {
             Buff = buff;
             Id = id;
         }
     }
}