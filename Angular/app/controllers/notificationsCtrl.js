"use strict";

SwapNShop.controller("notificationsCtrl", [
	"$scope",
	"$http",
	"$location",
	"EquipFactory",
	"AuthFactory",

  	function ($scope, $http, $location, EquipFactory, AuthFactory) {
  		let currMusician = AuthFactory.getUser();
  		$scope.allNotifications = [];

  		$http
	        .get(`http://localhost:49881/api/Notification/${currMusician.IdMusician}`)
	        .success(dates => {
	          $scope.allNotifications = dates;
	          console.log("notifications", $scope.allNotifications)
	    })
  	} 
]);