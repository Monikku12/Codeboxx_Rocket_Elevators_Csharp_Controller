namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public int callButtonsId;
        public string status;
        public int floor;
        public string direction;
        public CallButton(int callButtonsId, string _status, int _floor, string _direction)
        {
            this.callButtonsId = 1;
            this.status = _status;
            this.floor = _floor;
            this.direction = _direction;
        }
    }
}