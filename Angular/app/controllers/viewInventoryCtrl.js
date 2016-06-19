"use strict";

SwapNShop.controller("viewInventoryCtrl", [
  "$scope",
  "$http",
  "$location",
  "AuthFactory",

  function ($scope, $http, $location, AuthFactory) {
    $scope.equipment = [];
    $scope.selectedEquipment = {};
    let user = AuthFactory.getUser();
    console.log('user', user.IdMusician)
    var curr_musician_ID = parseInt(user.IdMusician);

    //Populate the modal
    $scope.showModal = function(id){
      console.log("show modal");
      console.log("id", id);
      //Get all the requested rental dates for this particular piece of equipment
      $http
      .get(`http://localhost:49881/api/RentalDates?EquipmentID=${id}`)
      .success(dates => {
        $scope.rentalDates = dates;
        console.log("rental Dates", $scope.rentalDates)
        //Split all the dates into readable dates
        for (var i = 0; i < $scope.rentalDates.length; i++) {
          var checkInDates = $scope.rentalDates[i].checkInDates.split(/\-|\T/)
          $scope.rentalDates[i].checkInDates = checkInDates[1] + "/" + checkInDates[2] + "/" + checkInDates[0];
          var checkOutDates = $scope.rentalDates[i].checkOutDates.split(/\-|\T/)
          $scope.rentalDates[i].checkOutDates = checkOutDates[1] + "/" + checkOutDates[2] + "/" + checkOutDates[0];
        }
        //Display the modal
        $('.modal-trigger').leanModal();
      });

      // Get the information for the equipment that the user clicked on
      $http
      .get(`http://localhost:49881/api/Equipment?EquipmentID=${id}`)
      .success(curEqu => {
        $scope.selectedEquipment = curEqu[0];
        console.log('selectedEquipment', $scope.selectedEquipment)
        //Display the modal
        $('.modal-trigger').leanModal();
      });
    }
  	
		$http
			.get(`http://localhost:49881/api/Equipment?MusicianID=${curr_musician_ID}`)
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});
			
	}
]);
