angular.module("courseApp")
    .factory("tradeSvc", ($http) => {
        return {
            getTradeWithCourse: () => {
                return $http({
                    method: "GET",
                    url: "/TradeData/TradesWithCourse",
                    headers: {
                        'Content-Type': "application/json"
                    }
                });
            }
        }
    });