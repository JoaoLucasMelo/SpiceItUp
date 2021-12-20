import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {
  async getSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    AppState.activeSteps = res.data
    logger.log('steps', AppState.activeSteps)
  }
  async addStep(data) {
    data['stepNumber'] = ((AppState.activeSteps.length) + 1)
      const res = await api.post('api/steps', data)
      logger.log('New Step', res.data)
      AppState.activeSteps.push(res.data)
  }
}
export const stepsService = new StepsService()