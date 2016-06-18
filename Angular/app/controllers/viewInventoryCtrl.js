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
        //Display the modal
        $('.modal-trigger').leanModal();
      });

      // Get the information for the equipment that the user clicked on
      $http
      .get(`http://localhost:49881/api/Equipment?EquipmentID=${id}`)
      .success(inv => {
        $scope.selectedEquipment = inv;
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
