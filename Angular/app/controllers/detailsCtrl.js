"use strict";

SwapNShop.controller("detailsCtrl", [
  "$scope",
  "$http",
  "$location",
  "EquipFactory",

  function ($scope, $http, $location, EquipFactory) {
  	$scope.equipment;
    let curEquipID = EquipFactory.getEquipment();
    console.log("curEquipID", curEquipID)

		$http
			.get(`http://localhost:49881/api/Equipment/${curEquipID}`)
			.success(inv => {
				$scope.equipment = inv[0];
				console.log("equipment", $scope.equipment);
			});
			
	}
]);
