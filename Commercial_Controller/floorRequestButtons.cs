namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class FloorRequestButton
    {
        public int floorRequestButtonID, floor;
        public string status, direction;
        public FloorRequestButton(int _id, string _status, int _floor, string _direction)
        {
            this.floorRequestButtonID = _id;
            this.status = _status;
            this.floor =  _floor;
            this.direction = _direction;
        }
    }
}