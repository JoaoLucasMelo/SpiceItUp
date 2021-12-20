import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {
  async getSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    let steps = AppState.activeSteps
    steps = res.data
    for (let i = 0; i < steps.length; i++) {
      steps[i].stepNumber = i + 1 ;
    }
    AppState.activeSteps = steps
    logger.log('steps', AppState.activeSteps)
  }
  async addStep(data) {
    data['stepNumber'] = ((AppState.activeSteps.length) + 1)
      const res = await api.post('api/steps', data)
      logger.log('New Step', res.data)
      AppState.activeSteps.push(res.data)
  }
  async removeStep(id){
    const res = await api.delete('api/steps/' + id)
    logger.log(res.data)
    let newApp = AppState.activeSteps.filter(s => s.id !== id)
    for (let i = 0; i < newApp.length; i++) {
      newApp[i].stepNumber = i + 1 ;
    }
    AppState.activeSteps = newApp
  }
}
export const stepsService = new StepsService()