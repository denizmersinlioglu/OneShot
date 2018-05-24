
public class Level {

	public int number;
	public LevelType type;
	public LevelStatus status;
	

	public Level(int number, LevelType type, LevelStatus status) {
		this.number = number;
		this.type = type;
		this.status = status;
	}

	public Level(){
		number = 0;
		type = LevelType.Gray;
		status = LevelStatus.locked;
	}

}
