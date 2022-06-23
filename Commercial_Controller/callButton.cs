namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public int ID, floor;
        public string status, direction;
        public CallButton(int _id, string _status, int _floor, string _direction)
        {
            this.ID = 1;
            this.status = _status;
            this.floor = _floor;
            this.direction = _direction;
        }
    }
}