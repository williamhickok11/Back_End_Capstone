"use strict";

SwapNShop.controller("detailsCtrl", [
  "$scope",
  "$http",
  "$location",
  "EquipFactory",
  "AuthFactory",

  function ($scope, $http, $location, EquipFactory, AuthFactory) {

    // Activate modal for rental
    $('.modal-trigger').leanModal();

    $scope.rentalDates;
  	$scope.equipment;
    let rentalDatesObject = {};
    let curr_equipment_ID = EquipFactory.getEquipment();
    let curMusicicanID = AuthFactory.getUser();
    
    rentalDatesObject.IdEquipment = curr_equipment_ID;
    rentalDatesObject.IdMusician = curMusicicanID.IdMusician;

    // Get access to the specific item the user clicked on
		$http
			.get(`http://localhost:49881/api/Equipment?EquipmentID=${curr_equipment_ID}`)
			.success(inv => {
				$scope.equipment = inv[0];
				console.log("equipment", $scope.equipment);
			});
		
    // post rental request to the database
    $scope.requestRent = function () {
      console.log("rent", $scope.rentalDates)

      var dateOUT = $scope.rentalDates.check_OUT_date.toISOString().slice(0, 19).replace('T', ' ').replace(/-/g, '/');
      var dateIN = $scope.rentalDates.check_IN_date.toISOString().slice(0, 19).replace('T', ' ').replace(/-/g, '/');
      
      // Build out the object to send to the database
      rentalDatesObject.checkOutDates = dateOUT;
      rentalDatesObject.checkInDates = dateIN;
      
      console.log("rentalDatesObject", rentalDatesObject)
      // post to the database
      $http({
        url:'http://localhost:49881/api/RentalDates',
        method: 'POST',
        data: JSON.stringify(rentalDatesObject)
      })
      .success(function newEquipment (){
        console.log('201 Created', rentalDatesObject)
        // $location.path("/homepage");
      })

      // Post that the equipment has been requested to be rented
      $http({
        url:`http://localhost:49881/api/Equipment/${curr_equipment_ID}`,
        method: 'PUT',
        // data: JSON.stringify($scope.equipment)
      })
      .success(function newEquipment (){
        console.log('201 updated')
      })	
    }
	}
]);


