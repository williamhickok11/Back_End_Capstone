"use strict";

SwapNShop.controller("notificationsCtrl", [
	"$scope",
	"$http",
	"$location",
	"EquipFactory",
	"AuthFactory",

  	function ($scope, $http, $location, EquipFactory, AuthFactory) {
  		$http
	        .get(`http://localhost:49881/api/Notifications`)
	        .success(dates => {
	          $scope.rentalDates = dates;
	          console.log("rental Dates", $scope.rentalDates)
	    }
  	} 
]);


