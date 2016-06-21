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

	    $scope.gotIt = function(id) {
	    	// Remove the notification from the page
	    	console.log("this.$parent",this.$parent)
	    	$(this).remove();
	    	// Delete the notification from the database
		    $http({
	            url:`http://localhost:49881/api/Notification/${id}`,
	            method: 'DELETE',
	          })
	    }
  	} 
]);