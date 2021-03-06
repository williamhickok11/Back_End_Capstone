"use strict";

SwapNShop.controller("detailsCtrl", [
  "$scope",
  "$http",
  "$location",
  "EquipFactory",
  "AuthFactory",
  "SelectedUserFactory",

  function ($scope, $http, $location, EquipFactory, AuthFactory, SelectedUserFactory) {

    $scope.curMusician = AuthFactory.getUser();
    // Activate modal for rental
    $('.modal-trigger').leanModal();

    $scope.rentalDates;
    $scope.equipment;
    let rentalDatesObject = {};
    $scope.currImage;
    let curr_equipment_ID = EquipFactory.getEquipment();
    console.log('currMusician', $scope.curMusician);
    
    rentalDatesObject.IdEquipment = curr_equipment_ID;
    rentalDatesObject.IdMusician = $scope.curMusician.IdMusician;

    // Get access to the specific item the user clicked on
		$http
			.get(`http://localhost:49881/api/Equipment?EquipmentID=${curr_equipment_ID}`)
			.success(inv => {
				$scope.equipment = inv[0];
        // Set variables to compare for ng-if
        $scope.MusicianID = $scope.curMusician.IdMusician
        $scope.EQMusicianID = $scope.equipment.musicianID
        $scope.currImage = $scope.equipment.picList[0].image
        console.log("MusicianID", $scope.MusicianID);
        console.log("EQMusicianID", $scope.EQMusicianID);
        console.log("equipment", $scope.equipment);
			});

    // thumbnail image click to make it the main image
    $scope.displayImg = function(img){
      $scope.currImage = img;
    }
		
    // post rental request to the database
    $scope.requestRent = function (ownerID) {
      console.log("rent", $scope.rentalDates)

      var dateOUT = $scope.rentalDates.check_OUT_date.toISOString().slice(0, 19).replace('T', ' ').replace(/-/g, '/');
      var dateIN = $scope.rentalDates.check_IN_date.toISOString().slice(0, 19).replace('T', ' ').replace(/-/g, '/');
      
      // Build out the object to send to the database
      rentalDatesObject.checkOutDates = dateOUT;
      rentalDatesObject.checkInDates = dateIN;
      rentalDatesObject.confirmed = false;
      
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
        url:`http://localhost:49881/api/Equipment/${curr_equipment_ID}/1`,
        method: 'PUT',
        // data: JSON.stringify($scope.equipment)
      })
      .success(function newEquipment (){
        console.log('201 updated')
      })

      // Post a new notification for the owner to see that the equipment has been requested
      let notificationCreation = {
        IdPostingMusician : $scope.curMusician.IdMusician,
        IdRecievingMusician : ownerID,
        newRentalRequest : true,
        description : `${$scope.curMusician.userName} sent you a rental request`,
      }
      // Post a deny request notification
      $http({
        url:'http://localhost:49881/api/Notification',
        method: 'POST',
        data: JSON.stringify(notificationCreation)
      })
    }

    $scope.goToPerson = function (id) {
      SelectedUserFactory.setUserId(id)
      if ($scope.curMusician.IdMusician == id){
        $location.path("/profile");
      } else {
         $location.path("/user_page");
      }
    }
	}
]);


