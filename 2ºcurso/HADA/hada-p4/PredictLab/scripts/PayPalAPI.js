
paypal.Buttons({
    createOrder: function (data, actions) {
        return actions.order.create({
            application_context: {
                shipping_preference: "NO_SHIPPING",
                user_action: "PAY_NOW"
            },
            purchase_units: [{
                amount: {
                    value: precioPaypal
                }
            }]
        });
    },

    onApprove: function (data, actions) {
        return actions.order.capture().then(function (details) {

            fetch('/Handler/PagoEfectuado.ashx', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    amount: details.purchase_units[0].amount.value,
                    currency: details.purchase_units[0].amount.currency_code,

                    usuario: userPaypal
                })
            }).then(response => response.text())
              .then(data => {
                  if (data.startsWith("Error")) {
                      alert(data);
                  } else {
                      alert('Pago realizado');
                  }
              });
        });
    }
}).render('#paypal-button-container');