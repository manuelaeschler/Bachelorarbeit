
public class Simulation {
	
	private static double x;
	private static double y;
	private static long n = 500000;
	private static int[][] field;
	private static RandomGenerator random;
	private static double inCircle;

	public static void main(String[] args){
		random = new RandomGenerator();
		start();
		System.out.println(inCircle*4/n);
		
	}

	private static void start() {
		field = new int[200][200];
		
		for(long i = 0; i < n; i++){
			x = random.getRandomNumber();
			y = random.getRandomNumber();
			
			if(x*x + y*y <= 1.0){
				inCircle++;
			}
			
		}
		
	}
}