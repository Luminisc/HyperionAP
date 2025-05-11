import { NgModule } from "@angular/core";
import { DemoClient } from "./api/api.generated";

@NgModule({
  providers:[
    DemoClient
  ]
})
export class ApiModule {}
