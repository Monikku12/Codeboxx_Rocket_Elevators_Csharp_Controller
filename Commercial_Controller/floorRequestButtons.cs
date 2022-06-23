namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class FloorRequestButton
    {
        public int floorRequestButtonID;
        public string status;
        public int buttonFloor;
        public string direction;
        public FloorRequestButton(int _id, string _status, int _buttonFloor, string _direction)
        {
            this.floorRequestButtonID = 1;
            this.status = _status;
            this.buttonFloor =  _buttonFloor;
            this.direction = _direction;
        }
    }
}