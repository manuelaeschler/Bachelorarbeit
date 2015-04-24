import java.math.*;

public class RandomGenerator {
	
	private double x;

	public RandomGenerator() {
		
	}
	
	public double getRandomNumber(){
		x = Math.random();
		x = x*2 -1;
		return x;
	}

}
