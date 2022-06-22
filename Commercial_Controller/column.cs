using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public int ID;
        public string status;
        public int amountOfFloors;
        public int amountOfElevators;
        public List<string> elevatorsList;
        public List<string> callButtonsList;
        public List<int> servedFloorsList;
        public bool isBasement;
        // public void createElevators(int amountOfFloors, int amountOfElevators);
        // public void createElevators(int amountOfFloors, bool isBasement);

        public Column(int _id, string _status, int _amountOfFloors, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {
            this.ID = _id;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.amountOfElevators = _amountOfElevators;
            this.elevatorsList = new List<string>();
            this.callButtonsList = new List<string>();
            this.servedFloorsList = _servedFloors;
            this.isBasement = _isBasement;
            // this.createElevators(_amountOfFloors, _amountOfElevators);
            // this.createCallButtons(_amountOfFloors, _isBasement);
            // this.requestElevator(int userPosition, string direction)
        }

        // public createCallButtons(_amountOfFloors, _isBasement)
        // {

        // }


        // public createElevators(_amountOfFloors, _amountOfElevators)


        
        // Simulate when a user press a button on a floor to go back to the first floor
        // public Elevator requestElevator(int userPosition, string direction)
        // {
            
        // }

        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            BestElevatorInformations bestElevator;
            int bestScore = 6;
            int referenceGap = 10000000;
            BestElevatorInformations bestElevatorInformations;

            if (requestedFloor == 1)
            {
                foreach (string elevator in this.elevatorsList) 
                    // The elevator is at the lobby and already has some requests. It is about to leave but has not yet departed.
                    if (1 == elevator.currentFloor && elevator.status == "stopped")
                    {
                        BestElevatorInformations bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, requestedFloor);
                    }
                     // The elevator is at the lobby and has no requests
                    else if (1 == elevator.currentFloor && elevator.status == "idle")
                    {
                        BestElevatorInformations bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is lower than me and is coming up. It means that I'm requesting an elevator to go to a basement, and the elevator is on it's way to me.
                    else if (1 > elevator.currentFloor && elevator == "up")
                    {
                        BestElevatorInformations bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is above me and is coming down. It means that I'm requesting an elevator to go to a floor, and the elevator is on it's way to me
                    else if (1 < elevator.currentFloor && elevator.direction == "down")
                    {
                        BestElevatorInformations bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is not at the first floor, but doesn't have any request
                    else if (elevator.status == "idle")
                    {
                        BestElevatorInformations bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is not available, but still could take the call if nothing better is found
                    else
                    {
                        BestElevatorInformations bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, requestedFloor);
                    }
                    bestElevator = bestElevatorInformations.bestElevator;
                    bestScore = bestElevatorInformations.bestScore;
                    referenceGap = bestElevatorInformations.referenceGap;
                    }
            }

        }


        public BestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck, string newElevator, BestElevatorInformations bestElevatorInformations, int floor)
        {            
            if (scoreToCheck < bestElevatorInformations.bestScore)
                {
                    bestElevatorInformations.bestScore = scoreToCheck;
                    bestElevatorInformations.bestElevator = newElevator;
                    bestElevatorInformations.referenceGap = Math.Abs(newElevator.currentFloor - floor);
                }
            else if (bestElevatorInformations.bestScore == scoreToCheck)
            {
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if (bestElevatorInformations.referenceGap > gap)
                {
                    bestElevatorInformations.bestElevator = newElevator;
                    bestElevatorInformations.referenceGap = gap;
                }
            }
            return bestElevatorInformations;
        }    
    }
    public class BestElevatorInformations
        {
        public string bestElevator;
        public int bestScore;
        public int referenceGap;

        public BestElevatorInformations(string _bestElevator, int _bestScore, int _referenceGap)
        {
            this.bestElevator = _bestElevator;
            this.bestScore = _bestScore;
            this.referenceGap = _referenceGap;
        }
    }
}