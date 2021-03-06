"use strict";

/* exported MusicHistory */

let SwapNShop = angular.module("SwapNShop", ["ngRoute", "ngFileUpload"])



SwapNShop.config(["$routeProvider",
  function ($routeProvider) {
    $routeProvider.
      when("/", {
        templateUrl: "partials/register.html",
        controller: 'registerCtrl',
      }).
      when("/homepage", {
        templateUrl: "partials/homepage.html",
        controller: 'mainCtrl'
      }).
      when("/user_page", {
        templateUrl: "partials/user_page.html",
        controller: 'userPageCtrl'
      }).
      when("/profile", {
        templateUrl: "partials/profile.html",
        controller: 'profileCtrl'
      }).
      when("/notifications", {
        templateUrl: "partials/notifications.html",
        controller: 'notificationsCtrl'
      }).
      when("/add_equipment", {
        templateUrl: "partials/add_equipment.html",
        controller: 'addEquipmentCtrl'
      }).
      when("/view_inventory", {
        templateUrl: "partials/view_inventory.html",
        controller: 'viewInventoryCtrl'
      }).
      when("/details_view", {
        templateUrl: "partials/details_view.html",
        controller: 'detailsCtrl'
      }).
      when("/confirmed_rental_dates", {
        templateUrl: "partials/confirmed_rental_dates.html",
        controller: 'confirmedRDCtrl'
      }).
      otherwise({
        redirectTo: "/"
      });
  }]);




