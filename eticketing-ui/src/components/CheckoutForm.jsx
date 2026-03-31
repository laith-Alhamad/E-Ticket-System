import "./CheckoutForm.css";
import { useState } from "react";
import axios from "axios";

function CheckoutForm({ ticket, apiBaseUrl, onSuccess }) {
  const [quantity, setQuantity] = useState(1);
  const [paymentMethod, setPaymentMethod] = useState("CreditCard");
  const [submitting, setSubmitting] = useState(false);

  const handleCheckout = async (e) => {
    e.preventDefault();

    try {
      setSubmitting(true);

      const payload = {
        ticketId: ticket.id,
        quantity: Number(quantity),
        paymentMethod: paymentMethod,
      };

      const response = await axios.post(`${apiBaseUrl}/Checkout`, payload);
      onSuccess(response.data);
    } catch (error) {
      console.error("Checkout error:", error);
      const message = error.response?.data?.message || "Checkout failed";
      alert(message);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page-wrapper">
    <div className="checkout-box">
      <h2>Checkout</h2>

      <p><strong>Ticket:</strong> {ticket.name}</p>
      <p><strong>Unit Price:</strong> {ticket.price}</p>

      <form onSubmit={handleCheckout}>
        <div style={{ marginBottom: "15px" }}>
          <label>Quantity:</label>
          <br />
          <input
            type="number"
            min="1"
            max="10"
            value={quantity}
            onChange={(e) => setQuantity(e.target.value)}
            required
          />
        </div>

        <div style={{ marginBottom: "15px" }}>
          <label>Payment Method:</label>
          <br />
          <select
            value={paymentMethod}
            onChange={(e) => setPaymentMethod(e.target.value)}
          >
            <option value="CreditCard">CreditCard</option>
            <option value="QR">QR</option>
          </select>
        </div>

        <p>
          <strong>Total:</strong> {ticket.price * quantity}
        </p>

        <button type="submit" disabled={submitting}>
          {submitting ? "Processing..." : "Confirm Checkout"}
        </button>
      </form>
    </div>
    </div>
  );
}

export default CheckoutForm;