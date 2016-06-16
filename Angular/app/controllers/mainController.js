"use strict";

SwapNShop.controller("mainCtrl", [
  "$scope",
  "$http",
  "$location",
  "EquipFactory",

  function ($scope, $http, $location, EquipFactory) {
  	$scope.equipment = [];

  	$scope.detailsView = function(equID){
  		console.log("equID", equID);
  		EquipFactory.setEquipment(equID);
  		$location.path("/details_view");
  	}

		$http
			.get('http://localhost:49881/api/Equipment')
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});
			
	}
]);
