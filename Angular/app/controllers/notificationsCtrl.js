"use strict";

SwapNShop.controller("notificationsCtrl", [
	"$scope",
	"$http",
	"$location",
	"EquipFactory",
	"AuthFactory",

  	function ($scope, $http, $location, EquipFactory, AuthFactory) {
  		let currMusician = AuthFactory.getUser();

  		$http
	        .get(`http://localhost:49881/api/Notifications/${currMusician.IdMusician}`)
	        .success(dates => {
	          $scope.rentalDates = dates;
	          console.log("rental Dates", $scope.rentalDates)
	    }
  	} 
]);


