public interface IHamiltonianPathSearch{
}

public class DPBitMaskingSearch{
    public void HamiltonianCycle(AdjGraph graph){
    }

    void print(bool[,] arr){
        for(int i = 0; i < arr.GetLength(0); i++){
            for(int j = 0; j < arr.GetLength(1); j++){
                Console.Write($"{arr[i,j]}");
            }
            Console.Write("\n");
        }
    }
}
