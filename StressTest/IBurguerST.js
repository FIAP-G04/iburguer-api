import http from 'k6/http';
import { sleep } from 'k6';
import { Counter } from 'k6/metrics';

export let options = {
  stages: [
    { duration: '2m', target: 200 }, // Ramp down to 200 VUs over 2 minutes
    { duration: '1m', target: 300 },
    { duration: '1m', target: 350 }, 
    { duration: '1m', target: 0 },  // Ramp down to 0 VUs over 1 minute
  ],
};

let myCounter = new Counter('my_counter');

export default function () {
  
  // Create shoppingCart
  const payload = { customerId: '041b23ad-3b11-441e-bab8-bb9270a09ae1' };
  const params = { headers: { 'Content-Type': 'application/json' } };
  const response1 = http.post('http://localhost:5000/api/carts', JSON.stringify(payload), params);
  console.log(`Create shoppingCart - Status Code: ${response1.status}`);
  myCounter.add(1);

  sleep(1);

  // Get Orders
  const response2 = http.get('http://localhost:5000/api/orders');
  console.log(`Get Orders - Status Code: ${response2.status}`);
  myCounter.add(1);
  sleep(1);

   // Get queued Orders
   const response3 = http.get('http://localhost:5000/api/orders/queue');
   console.log(`Get queued Orders - Status Code: ${response3.status}`);
   myCounter.add(1);
   sleep(1);
 
}

// Print the total number of requests after the test completes
export function teardown(data) {
  console.log(`Total Requests: ${myCounter.value}`);
}
