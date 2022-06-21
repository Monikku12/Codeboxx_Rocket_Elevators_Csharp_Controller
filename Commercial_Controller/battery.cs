using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    { 
    public int ID;
    public string status;
    public List<int> columnsList;
    public List<int> floorRequestsButtonsList;
 
    // public void IF (int amountOfBasements > 0);    
    // public void createFloorRequestButtons(int amountOfFloors);
    // public void createColumns(int amountOfColumns, int amountOfFloors, int amountOfElevatorPerColumn);
    // public void createBasementFloorRequestButtons(int amountOfBasements);
    // public void createBasementColumn(int amountOfBasements, int amountOfElevatorPerColumn);



    public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {             
            this.ID = _id;
            this.status = "online";
            this.columnsList = new List<int>();
            this.floorRequestsButtonsList = new List<int>();
    
//             IF (_amountOfBasements > 0);
//             {
//                 createBasementFloorRequestButtons(_amountOfBasements);
//                 createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
//                 _amountOfColumns--;
//             }
//             createFloorRequestButtons(_amountOfFloors);
//             createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);

        }

        // public Column findBestColumn(int _requestedFloor)
        // {
            
        // }
        // //Simulate when a user press a button at the lobby
        // public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        // {
            
        // }
    }
}

