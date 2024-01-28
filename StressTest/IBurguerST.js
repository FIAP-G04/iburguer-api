import http from 'k6/http';
import { sleep } from 'k6';
import { Counter } from 'k6/metrics';

export let options = {
  stages: [
    { duration: '6m', target: 10 }
  ],
};

let requestCounter = Counter('total_requests');
let errorCounter = Counter('total_errors');

export default function () {
  
  // Create shoppingCart
  const payload = { customerId: '041b23ad-3b11-441e-bab8-bb9270a09ae1' };
  const params = { headers: { 'Content-Type': 'application/json' } };
  const response1 = http.post('http://localhost/api/carts', JSON.stringify(payload), params);
  logResponse('Create shoppingCart', response1);
  sleep(1);

  // Get Orders
  const response2 = http.get('http://localhost/api/orders');
  logResponse('Get Orders', response2);
  sleep(1);

   // Get queued Orders
   const response3 = http.get('http://localhost/api/orders/queue');
   logResponse('Get queued Orders', response3);
   sleep(1);
 
}

// Function to log response details and errors
function logResponse(requestName, response) {
  console.log(`${requestName} - Status Code: ${response.status}`);
  requestCounter.add(1);

  // Check for errors and log error details
  if (response.status !== 200) {
    console.error(`${requestName} - Error - Status Code: ${response.status}`);
    errorCounter.add(1);
  }
}
