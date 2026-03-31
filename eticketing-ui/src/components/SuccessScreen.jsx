import "./SuccessView.css";


function SuccessView({ result, onBack }) {
  return (
  <div className="success-wrapper">
  <div className="success-box"> 
    <div
      style={{
        marginTop: "30px",
        border: "1px solid green",
        padding: "20px",
        borderRadius: "10px",
        maxWidth: "500px",
        backgroundColor: "#f6fff6",
      }}
    >
      <h2>Payment Successful</h2>

      <p><strong>Order ID:</strong> {result.orderId}</p>
      <p><strong>Transaction ID:</strong> {result.transactionId}</p>
      <p><strong>Transaction Reference:</strong> {result.transactionReference}</p>
      <p><strong>Amount:</strong> {result.amount}</p>
      <p><strong>Order Status:</strong> {result.orderStatus}</p>
      <p><strong>Payment Status:</strong> {result.paymentStatus}</p>
      <p><strong>Timestamp:</strong> {result.timestamp}</p>
      <p><strong>Message:</strong> {result.message}</p>

      <button onClick={onBack}>Back to Tickets</button>
    </div>
    </div>
    </div>
  );
}

export default SuccessView;