"use strict";

SwapNShop.controller("detailsCtrl", [
  "$scope",
  "$http",
  "$location",
  "EquipFactory",

  function ($scope, $http, $location, EquipFactory) {
    // Activate modal for rental
    $('.modal-trigger').leanModal();

    $scope.rentalDates;
  	$scope.equipment;

    let curEquipID = EquipFactory.getEquipment();
    console.log("curEquipID", curEquipID)

    // Get access to the specific item the user clicked on
		$http
			.get(`http://localhost:49881/api/Equipment/${curEquipID}`)
			.success(inv => {
				$scope.equipment = inv[0];
				console.log("equipment", $scope.equipment);
			});
		
    // post rental request to the database
    $scope.requestRent = function (files) {
      console.log("rent")
      // $http({
      //   url:'http://localhost:49881/api/Equipment',
      //   method: 'POST',
      //   data: JSON.stringify($scope.equipment)
      // })
      // .success(function newEquipment (){
      //   console.log('201 Created', newEquipment)
      //     $location.path("/homepage");
      // })	
    }
	}
]);
