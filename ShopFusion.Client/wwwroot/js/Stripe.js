RedirectUserToStripePayment = function (id) {
	var stripe = Stripe("pk_test_51QZtMhDyP50ifadOEJZ9iBhaJl27cZEnCWbToJdg6jqPl4eCYXEXnzZgnvfTtPspeiQtQ5m0zoNgNK7fHdCUhZBT00Oidkbt7v");
	stripe.redirectToCheckout({ sessionId: id });
}