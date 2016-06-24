"use strict";

SwapNShop.controller("confirmedRDCtrl", [
    "$scope",
    "$http",
    "$location",
    "EquipFactory",
    "AuthFactory",
    "SelectedUserFactory",

    function ($scope, $http, $location, EquipFactory, AuthFactory, SelectedUserFactory) {
        let currMusician = AuthFactory.getUser();
        $scope.confirmedRentalDates = [];
        
        $scope.goToPerson = function (id) {
            SelectedUserFactory.setUserId(id)
            if (currMusician.IdMusician == id){
                $location.path("/profile");
            } else {
                $location.path("/user_page");
            }
        }

        $http
            .get(`http://localhost:49881/api/RentalDates?MusicianID=${currMusician.IdMusician}&newRentalCheck=false`)
            .success(dates => {
                $scope.confirmedRentalDates = dates;
                console.log("rental Dates", $scope.confirmedRentalDates)
                //Split all the dates into readable dates
                for (var i = 0; i < $scope.confirmedRentalDates.length; i++) {
                    var checkInDates = $scope.confirmedRentalDates[i].checkInDates.split(/\-|\T/)
                    $scope.confirmedRentalDates[i].checkInDates = checkInDates[1] + "/" + checkInDates[2] + "/" + checkInDates[0];
                    var checkOutDates = $scope.confirmedRentalDates[i].checkOutDates.split(/\-|\T/)
                    $scope.confirmedRentalDates[i].checkOutDates = checkOutDates[1] + "/" + checkOutDates[2] + "/" + checkOutDates[0];
                }
            })

        $scope.cancelRequest = function(rentalDateId, renterId) {
            // Remove the notification from the page
            let itemToDelete = this.date;
            $scope.confirmedRentalDates.splice($scope.confirmedRentalDates.indexOf(itemToDelete), 1);
            // Delete the notification from the database
            $http({
                url:`http://localhost:49881/api/RentalDates/${rentalDateId}`,
                method: 'DELETE',
            })
            .success(function(){
                // Post a notification to inform the user that their request has been canceled`
                let notificationCreation = {
                    IdPostingMusician : currMusician.IdMusician,
                    IdRecievingMusician : renterId,
                    description : `${currMusician.userName} has canceled your rental request`,
                }
                // Post a cancel request notification
                $http({
                    url:'http://localhost:49881/api/Notification',
                    method: 'POST',
                    data: JSON.stringify(notificationCreation)
                })
            })
        }
    }
]);






