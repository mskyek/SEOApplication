//class SEOContext {
//    GetSearchResults(searchTerm: string, callbackSuccess: (data: Object) => any, callbackFailure: () => any) {
//        $.ajax({
//            type: "POST",
//            url: "/Home/GetSearchResults",
//            dataType: "json",
//            cpmtemtType: "application/json",
//            data: JSON,
//            cache: false,
//            success: (data: any): void => {
//                callbackSuccess(data);
//            },
//            errpo: (message: any): void => {
//                callbackFailure();
//            }
//        })
//    }
//}