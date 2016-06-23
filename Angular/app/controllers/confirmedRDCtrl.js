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
            $location.path("/user_page");
        }

        $http
            .get(`http://localhost:49881/api/RentalDates?MusicianID=${currMusician.IdMusician}`)
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
    }

]);






