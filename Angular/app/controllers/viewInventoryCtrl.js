"use strict";

SwapNShop.controller("viewInventoryCtrl", [
  "$scope",
  "$http",
  "$location",

  function ($scope, $http, $location) {
  	$scope.equipment = [];
  	let user = getUser()

		$http
			.get(`http://localhost:49881/api/Equipment`)
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});
			
	}
]);
