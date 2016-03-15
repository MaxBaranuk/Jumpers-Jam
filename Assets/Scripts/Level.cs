
[System.Serializable]
public class Level {
    public int points;
    public int stars;
    public bool isOpen;

    public Level(int points, int stars, bool isOpen) {
        this.points = points;
        this.stars = stars;
        this.isOpen = isOpen;
    }
	
}
