"use strict";

SwapNShop.controller("mainCtrl", [
    "$scope",
    "$http",
    "$location",
    "EquipFactory",
    "AuthFactory",

    function ($scope, $http, $location, EquipFactory, AuthFactory) {
	  	let currMusician = AuthFactory.getUser();
	  	$scope.equipment = [];
	  	console.log(currMusician);

	  	$scope.detailsView = function(equID){
	  		console.log("equID", equID);
	  		EquipFactory.setEquipment(equID);
	  		$location.path("/details_view");
	  	}

	  	// Get all the equipment to show on the page
		$http
			.get('http://localhost:49881/api/Equipment')
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});
				
	}
]);
