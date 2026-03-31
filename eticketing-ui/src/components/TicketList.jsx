import "./TicketList.css";

function TicketList({ tickets, onSelectTicket }) {
  return (
    <div className="ticket-container">
      <h2>Available Tickets</h2>

      {tickets.length === 0 ? (
        <p>No tickets available.</p>
      ) : (
        <div className="ticket-list">
          {tickets.map((ticket) => (
            <div key={ticket.id} className="ticket-card">
              <h3>{ticket.name}</h3>
              <p><strong>Price:</strong> {ticket.price}</p>
              <p><strong>Remaining:</strong> {ticket.remainingQuota}</p>

              <button onClick={() => onSelectTicket(ticket)}>
                Buy
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}


export default TicketList;